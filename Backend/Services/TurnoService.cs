using DB;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

        public TurnoService(FirenzeContext context, CrearTurnoValidator crearTurnoValidator, ActualizarTurnoValidator actualizarTurnoValidator)
        {
            _context = context;
            _crearTurnoValidator = crearTurnoValidator;
            _actualizarTurnoValidator = actualizarTurnoValidator;
        }

        public async Task<bool> ExisteTurnoAsync(DateOnly fecha, TimeOnly hora)
        {
            return await _context.Turnos.AnyAsync(x => x.Fecha_Tu == fecha && x.Hora_Tu == hora);
        }

        public async Task<Result<TurnoDTO>> CrearTurnoAsync(CrearTurnoDTO turnoDTO)
        {
            FluentValidation.Results.ValidationResult result = await _crearTurnoValidator.ValidateAsync(turnoDTO);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(e => new Error(e.ErrorMessage, e.PropertyName))
                    .ToList();

                return Result<TurnoDTO>.Failure(errors);
            }
            /*
            var turno = new Turno
            {
                Fecha_Tu = turnoDTO.Fecha_Tu,
                Hora_Tu = turnoDTO.Hora_Tu,
                Duracion_Tu = turnoDTO.Duracion_Tu,
                Precio_Total_Tu = 0,
                Seña_Tu = 0,
                Id_Usuario_Tu = turnoDTO.Id_Usuario_Tu,
                Id_Estado_Turno_Tu = 3
            };

            _context.Add(turno);
            await _context.SaveChangesAsync();

            decimal precioTotal = 0;
            var trabajosXTurnos = new List<TrabajoXTurno>();
            var descripcionesTrabajo = new List<string>();

            foreach (int idTrabajo in turnoDTO.Ids_Trabajo)
            {
                var trabajo = await _context.Trabajos.FindAsync(idTrabajo);

                if(trabajo != null)
                {
                    trabajosXTurnos.Add(new TrabajoXTurno
                    {
                        Id_Turno_Tt = turno.Id_Turno_Tu,
                        Id_Trabajo_Tt = trabajo.Id_Trabajo_Tr
                    });

                    precioTotal += trabajo.Precio_Tr;
                    descripcionesTrabajo.Add(trabajo.Descripcion_Tr);
                }

            }

            if(trabajosXTurnos.Any())
            {
                await _context.AddRangeAsync(trabajosXTurnos);
            }

            turno.Precio_Total_Tu = precioTotal;
            turno.Seña_Tu = precioTotal * 0.10m;

            var turnoCreado = new TurnoDTO
            {
                Id_Turno_Tu = turno.Id_Turno_Tu,
                Fecha_Tu = turno.Fecha_Tu,
                Hora_Tu = turno.Hora_Tu,
                Duracion_Tu = turno.Duracion_Tu,
                Precio_Total_Tu = precioTotal,
                Seña_Tu = turno.Seña_Tu,
                Id_Usuario_Tu = turno.Id_Usuario_Tu,
                Id_Estado_Turno_Tu = turno.Id_Estado_Turno_Tu,
                DescripcionesTrabajo = descripcionesTrabajo
            };

            return Result<TurnoDTO>.Success(turnoCreado);
            */

            throw new NotImplementedException();

        }
        public async Task<Result<TurnoDTO>> GetAllTurnosAsync(TurnoDTO turnoDTO)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<TurnoDTO>> GetTurnoAsync(int idTurno)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> ActualizarTurnoAsync(int idTurno, ActualizarTurnoDTO turnoDTO)
        {
            throw new NotImplementedException();
        }


        public async Task<Result<bool>> EliminarTurnoAsync(int idTurno)
        {
            throw new NotImplementedException();
        }

    }
}
