using DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services
{
    public class TrabajoXTurnoService : ITrabajoXTurnoService
    {
        private readonly FirenzeContext _context;

        public TrabajoXTurnoService(FirenzeContext context)
        {
            _context = context;
        }

        public async Task<bool> AsignarTrabajosTurnoAsync(int idTurno, List<int> idsTrabajo)
        {

            List<TrabajoXTurno> trabajosXTurnos = new List<TrabajoXTurno>();

            foreach(int idTrabajo in idsTrabajo)
            {
                trabajosXTurnos.Add(new TrabajoXTurno{
                    Id_Turno_Tt = idTurno,
                    Id_Trabajo_Tt = idTrabajo,
                });
            }

            _context.TrabajosXTurnos.AddRange(trabajosXTurnos);
            
            var rowsAffected = await _context.SaveChangesAsync();  

            if(rowsAffected == 0 || !trabajosXTurnos.Any())
            {
                return false;
            }

            return true;
        }
    }
}
