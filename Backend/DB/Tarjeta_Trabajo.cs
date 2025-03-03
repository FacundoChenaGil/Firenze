using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Tarjeta_Trabajo
    {
        [Key]
        public int Id_Tarjeta_Trabajo_Tar { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nombre_Tar { get; set; }

        [Required]
        [MaxLength(40)]
        public string Descripcion_Tar { get; set; }

        [Required]
        [MaxLength(50)]
        public string Imagen_URL_Tar { get; set; }

        public int Id_Trabajo_Tar { get; set; }

        [ForeignKey("Id_Trabajo_Tar")]
        public virtual Trabajo Trabajo { get; set; }
    }
}
