using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Tipo_Usuario
    {
        [Key]
        public int Id_Tipo_Usuario_Tus { get; set; }

        [Required]
        [MaxLength(20)]
        public string Descripcion_Tus { get; set; }
        public virtual ICollection<Usuario> Usuarios  { get; set; }
    }
}
