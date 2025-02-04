using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Trabajo
    {
        [Key]
        public int Id_Trabajo_Tr { get; set; }

        [Required]
        [MaxLength(100)]
        public string Descripcion_Tr { get; set; }

        public decimal Precio_Tr { get; set; }
        public TimeSpan? Duracion_Tr { get; set; }
        public bool Adicional_Tr { get; set; }
        
        public int Id_Tipo_Trabajo_Tr { get; set; }

        [ForeignKey("Id_Tipo_Trabajo_Tr")]
        public virtual Tipo_Trabajo Tipo_Trabajo { get; set; }
        public virtual ICollection<Tarjeta_Trabajo> Tarjetas_Trabajo { get; set; }
        public virtual ICollection<TrabajoXTurno> TrabajosXTurnos { get; set; }
    }
}
