using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ActualizarUsuarioDTO
    {
        public string Nombre_Usuario_Us { get; set; }
        public string Contraseña_Us { get; set; }
        public string? Nombre_Us { get; set; }
        public string? Apellido_Us { get; set; }
        public string? Telefono_Us { get; set; }
        public int Id_Tipo_Usuario_Us { get; set; }
    }
}
