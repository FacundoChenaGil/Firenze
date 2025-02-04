using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Estado_Turno
    {
        [Key]
        public int Id_Estado_Turno_Et { get; set; }
        public string Descripcion_Et { get; set; }
        public virtual ICollection<Turno> Turnos { get; set; }
    }
}
