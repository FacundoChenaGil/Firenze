using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services
{
    public interface ITrabajoXTurnoService
    {
        public Task<bool> AsignarTrabajosTurnoAsync(int idTurno, List<int> idsTrabajo);
    }
}
