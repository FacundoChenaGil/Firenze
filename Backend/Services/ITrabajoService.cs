using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services
{
    public interface ITrabajoService
    {
        public Task<Result<CrearTrabajoDTO>> CrearTrabajoAsync(CrearTrabajoDTO trabajoDTO);
        public Task<Result<List<TrabajoDTO>>> GetAllTrabajosAsync();
        public Task<Result<TrabajoDTO>> GetTrabajoAsync(int idTrabajo);

    }
}
