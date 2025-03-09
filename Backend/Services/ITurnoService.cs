using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services
{
    public interface ITurnoService
    {
        public Task<List<TimeOnly>> ObtenerTurnosDisponiblesAsync(DateOnly fechaIngresada);
        public Task<Result<TurnoCalculosResponseDTO>> CalcularTurnoAsync(List<int> idsTrabajo);
        public Task<Result<bool>> CrearTurnoAsync(CrearTurnoDTO turnoDTO);
        public Task<Result<List<TurnoDTO>>> GetAllTurnosAsync();
        public Task<Result<TurnoDTO>> GetTurnoAsync(int idTurno);
        public Task<Result<bool>> ActualizarTurnoAsync(int idTurno, ActualizarTurnoDTO turnoDTO);
        public Task<Result<bool>> EliminarTurnoAsync(int idTurno);
    }
}
