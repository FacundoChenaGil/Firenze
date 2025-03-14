using DB;
using DTOs;
using FluentValidation;
using Microsoft.AspNetCore.SignalR.Protocol;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Validations;

namespace Services
{
    public class TurnoService : ITurnoService
    {
        private readonly FirenzeContext _context;
        private readonly CrearTurnoValidator _crearTurnoValidator;
        private readonly ActualizarTurnoValidator _actualizarTurnoValidator;
        private readonly ITrabajoXTurnoService _trabajoXTurnoService;
       

        public TurnoService(FirenzeContext context, CrearTurnoValidator crearTurnoValidator, ActualizarTurnoValidator actualizarTurnoValidator, ITrabajoXTurnoService trabajoXTurnoService)
        {
            _context = context;
            _crearTurnoValidator = crearTurnoValidator;
            _actualizarTurnoValidator = actualizarTurnoValidator;
            _trabajoXTurnoService = trabajoXTurnoService;
        }

        public async Task<List<TimeOnly>> ObtenerTurnosDisponiblesAsync(DateOnly fechaIngresada)
        {
            TimeOnly horaApertura = new(8, 0);
            TimeOnly horaCierre = new(20, 0);
            TimeSpan duracionBloque = TimeSpan.FromHours(1);

            var estadosOcupados = new HashSet<int> { 2, 3, 4 };

            var horariosOcupados = await _context.Turnos
                .Where(t => t.Fecha_Tu == fechaIngresada && estadosOcupados.Contains(t.Id_Estado_Turno_Tu))
                .Select(t => new
                {
                    HoraInicio = t.Hora_Tu,
                    HoraFin = t.Hora_Tu.Add(t.Duracion_Tu.GetValueOrDefault())
                })
                .ToListAsync();

            List<TimeOnly> horariosDisponibles = new List<TimeOnly>();

            for(TimeOnly hora = horaApertura; hora <= horaCierre; hora = hora.Add(duracionBloque))
            {
                bool ocupado = horariosOcupados.Any(h => hora >= h.HoraInicio && hora < h.HoraFin);

                if(!ocupado)
                {
                    horariosDisponibles.Add(hora);
                }
            }

            return horariosDisponibles;
        }

        public async Task<Result<TurnoCalculosResponseDTO>> CalcularTurnoAsync(List<int> idsTrabajo)
        {
            TimeSpan duracionTotal = TimeSpan.Zero;
            decimal PrecioTotal = 0;
            decimal seña;

            List<TrabajoCalculoDTO> trabajosTurno = await _context.Trabajos
                .Where(t => idsTrabajo.Contains(t.Id_Trabajo_Tr))
                .Select(t => new TrabajoCalculoDTO
                {
                    Precio = t.Precio_Tr,
                    Duracion = t.Duracion_Tr
                })
                .ToListAsync(); 

            if(!trabajosTurno.Any())
            {
                return Result<TurnoCalculosResponseDTO>.Failure(new List<Error> { new Error("No se encontraron Trabajos con esos IDs.", "CalcularTurnoAsync") });
            }

            foreach(TrabajoCalculoDTO trabajo in trabajosTurno)
            {
                duracionTotal += trabajo.Duracion ?? TimeSpan.Zero;
                PrecioTotal += trabajo.Precio;
            }

            seña = PrecioTotal * 0.10m;

            TurnoCalculosResponseDTO turnoCalculo = new TurnoCalculosResponseDTO
            {
                Duracion = duracionTotal,
                PrecioTotal = PrecioTotal,
                Seña = seña
            };

            return Result<TurnoCalculosResponseDTO>.Success(turnoCalculo);
        }

        public async Task<Result<bool>> CrearTurnoAsync(CrearTurnoDTO turnoDTO)
        {
            FluentValidation.Results.ValidationResult result = await _crearTurnoValidator.ValidateAsync(turnoDTO);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(e => new Error(e.ErrorMessage, e.PropertyName))
                    .ToList();

                return Result<bool>.Failure(errors);
            }

            var turnoCreado = new Turno
            {
                Fecha_Tu = turnoDTO.Fecha_Tu,
                Hora_Tu = turnoDTO.Hora_Tu,
                Duracion_Tu = turnoDTO.Duracion_Tu,
                Precio_Total_Tu = turnoDTO.Precio_Total_Tu,
                Seña_Tu = turnoDTO.Seña_Tu,
                Id_Usuario_Tu = turnoDTO.Id_Usuario_Tu,
                Id_Estado_Turno_Tu = 3
            };

            _context.Add(turnoCreado);
            int rowsAffected = await _context.SaveChangesAsync();

            if(rowsAffected == 0)
            {
                return Result<bool>.Failure(new List<Error> { new Error("No se pudo generar el turno.", "CrearTurnoAsync") });
            }

            bool trabajosAsignados = await _trabajoXTurnoService.AsignarTrabajosTurnoAsync(turnoCreado.Id_Turno_Tu, turnoDTO.Ids_Trabajo);

            if(!trabajosAsignados)
            {
                return Result<bool>.Failure(new List<Error> { new Error("No se pudieron asignar los Trabajos.", "CrearTurnosAsync") });
            }

            return Result<bool>.Success(true);

        }
        public async Task<Result<List<TurnoDTO>>> GetAllTurnosAsync()
        {
            var turnos = await _context.Turnos
                .GroupJoin(
                _context.TrabajosXTurnos,
                t => t.Id_Turno_Tu,
                tt => tt.Id_Turno_Tt,
                (t, trabajos) => new TurnoDTO
                {
                    Id_Turno_Tu = t.Id_Turno_Tu,
                    Fecha_Tu = t.Fecha_Tu,
                    Hora_Tu = t.Hora_Tu,
                    Seña_Tu = t.Seña_Tu,
                    Duracion_Tu = t.Duracion_Tu,
                    Precio_Total_Tu = t.Precio_Total_Tu,
                    Id_Usuario_Tu = t.Id_Usuario_Tu,
                    Id_Estado_Turno_Tu = t.Id_Estado_Turno_Tu,
                    Ids_Trabajo = trabajos.Select(t => t.Id_Trabajo_Tt).ToList()
                })
                .ToListAsync();
                
            if(turnos == null || !turnos.Any())
            {
                return Result<List<TurnoDTO>>.Failure(new List<Error> { new Error("No se encontraron Turnos.", "GetAllTurnosAsync") });
            }

            return Result<List<TurnoDTO>>.Success(turnos);
        }

        public async Task<Result<TurnoDTO>> GetTurnoAsync(int idTurno)
        {
            var turno = await _context.Turnos
                .GroupJoin(
                _context.TrabajosXTurnos,
                t => t.Id_Turno_Tu,
                tt => tt.Id_Turno_Tt,
                (t, trabajos) => new TurnoDTO
                {
                    Id_Turno_Tu = t.Id_Turno_Tu,
                    Fecha_Tu = t.Fecha_Tu,
                    Hora_Tu = t.Hora_Tu,
                    Seña_Tu = t.Seña_Tu,
                    Duracion_Tu = t.Duracion_Tu,
                    Precio_Total_Tu = t.Precio_Total_Tu,
                    Id_Usuario_Tu = t.Id_Usuario_Tu,
                    Id_Estado_Turno_Tu = t.Id_Estado_Turno_Tu,
                    Ids_Trabajo = trabajos.Select(t => t.Id_Trabajo_Tt).ToList()
                })
                .FirstOrDefaultAsync();

            if (turno == null)
            {
                return Result<TurnoDTO>.Failure(new List<Error> { new Error($"No se encontro el Turno con id: {idTurno}.", "GetTurnoAsync") });
            }

            return Result<TurnoDTO>.Success(turno);
        }

        public async Task<Result<bool>> ActualizarTurnoAsync(int idTurno, ActualizarTurnoDTO turnoDTO)
        {
            FluentValidation.Results.ValidationResult result = await _actualizarTurnoValidator.ValidateAsync(turnoDTO);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(e => new Error(e.ErrorMessage, e.PropertyName))
                    .ToList();

                return Result<bool>.Failure(errors);
            }

            var turnoActualizado = await _context.Turnos.FirstOrDefaultAsync(t => t.Id_Turno_Tu == idTurno);

            if(turnoActualizado == null)
            {
                return Result<bool>.Failure(new List<Error> { new Error($"No se encontro el usuario con id: {idTurno}.", "ActualizarTurnoAsync") });
            }

            turnoActualizado.Fecha_Tu = turnoDTO.Fecha_Tu;
            turnoActualizado.Hora_Tu = turnoDTO.Hora_Tu;
            turnoActualizado.Seña_Tu = turnoDTO.Seña_Tu;
            turnoActualizado.Duracion_Tu = turnoDTO.Duracion_Tu;
            turnoActualizado.Precio_Total_Tu = turnoDTO.Precio_Total_Tu;
            turnoActualizado.Id_Estado_Turno_Tu = turnoDTO.Id_Estado_Turno_Tu;

            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }


        public async Task<Result<bool>> EliminarTurnoAsync(int idTurno)
        {
            var turno = await _context.Turnos.FirstOrDefaultAsync(t => t.Id_Turno_Tu == idTurno);

            if(turno == null)
            {
                return Result<bool>.Failure(new List<Error> { new Error($"No se encontro el turno con id: {idTurno}.", "EliminarTurnoAsync") });
            }

            turno.Id_Estado_Turno_Tu = 2;

            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);

        }

    }
}
