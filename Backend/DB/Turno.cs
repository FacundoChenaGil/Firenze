using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Turno
    {
        [Key]
        public int Id_Turno_Tu { get; set; }

        [Required]
        public DateOnly Fecha_Tu { get; set; }

        [Required]
        public TimeOnly Hora_Tu { get; set; }
        public decimal Seña_Tu { get; set; }
        public TimeSpan? Duracion_Tu { get; set; }

        [Required]
        public decimal Precio_Total_Tu { get; set; }

        public int Id_Usuario_Tu { get; set; }

        public int Id_Estado_Turno_Tu { get; set; }
        [ForeignKey("Id_Usuario_Tu")]
        public virtual Usuario Usuario { get; set; }

        [ForeignKey("Id_Estado_Turno_Tu")]
        public virtual Estado_Turno Estado_Turno { get; set; }
        public virtual ICollection<TrabajoXTurno> TrabajosXTurnos { get; set; }

    }
}
