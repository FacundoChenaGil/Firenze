using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class TrabajoXTurno
    {
        public int Id_Turno_Tt { get; set; }
        public int Id_Trabajo_Tt { get; set; }

        [ForeignKey("Id_Turno_Tt")]
        public virtual Turno Turno { get; set; }

        [ForeignKey("Id_Trabajo_Tt")]
        public virtual Trabajo Trabajo { get; set; }
    }
}
