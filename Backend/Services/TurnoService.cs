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
                bool ocupado = horariosOcupados.Any(h => hora >= h.HoraInicio && hora <= h.HoraFin);

                if(!ocupado)
                {
                    horariosDisponibles.Add(hora);
                }
            }

            return horariosDisponibles;
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
