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
        public Task<bool> ExisteTurnoAsync(DateOnly fecha, TimeOnly hora);
        public Task<Result<TurnoDTO>> CrearTurnoAsync(CrearTurnoDTO turnoDTO);
        public Task<Result<TurnoDTO>> GetAllTurnosAsync(TurnoDTO turnoDTO);
        public Task<Result<TurnoDTO>> GetTurnoAsync(int idTurno);
        public Task<Result<bool>> ActualizarTurnoAsync(int idTurno, ActualizarTurnoDTO turnoDTO);
        public Task<Result<bool>> EliminarTurnoAsync(int idTurno);
    }
}
