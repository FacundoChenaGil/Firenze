using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Tipo_Trabajo
    {
        [Key]
        public int Id_Tipo_Trabajo_Ttr { get; set; }
        public string Descripcion_Ttr { get; set; }
        public virtual ICollection<Trabajo> Trabajos { get; set; }
    }
}
