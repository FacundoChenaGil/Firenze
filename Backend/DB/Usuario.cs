using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Usuario
    {
        [Key]
        public int Id_Usuario_Us { get; set; }

        [Required]
        [MaxLength(20)]
        public string Nombre_Usuario_Us { get; set; }

        [Required]
        [MaxLength(64)]
        public string Contraseña_Us { get; set; }

        [MaxLength(20)]
        public string? Nombre_Us { get; set; }

        [MaxLength(20)]
        public string? Apellido_Us { get; set; }

        [MaxLength(20)]
        public string? Telefono_Us { get; set; }

        [Required]
        [MaxLength(20)]
        public string Correo_Electronico_Us { get; set; }
        public int Id_Tipo_Usuario_Us { get; set; }
        [ForeignKey("Id_Tipo_Usuario_Us")]
        public virtual Tipo_Usuario Tipo_Usuario { get; set; }

        public virtual ICollection<Turno> Turnos { get; set; }

    }
}
