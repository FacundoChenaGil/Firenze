using DB;
using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services
{
    public interface ITipoTrabajoService
    {
        public Task<Result<List<TipoTrabajoDTO>>> GetAllTiposTrabajoAsync();
    }
}
