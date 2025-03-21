using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services
{
    public interface ITarjetaTrabajoService
    {
        public Task<Result<bool>> CrearTarjetaTrabajoAsync(CrearTarjetaTrabajoDTO tarjetaDTO);
        public Task<Result<List<TarjetaTrabajoDTO>>> GetAllTarjetasTrabajoAsync();
        public Task<Result<TarjetaTrabajoDTO>> GetTarjetaTrabajoAsync(int idTarjetaTrabajo);
        public Task<Result<bool>> ActualizarTarjetaTrabajoAsync(int idTarjetaTrabajo, ActualizarTarjetaTrabajoDTO tarjetaDTO);
        public Task<Result<bool>> EliminarTarjetaTrabajoAsync(int idTarjetaTrabajo);

    }
}
