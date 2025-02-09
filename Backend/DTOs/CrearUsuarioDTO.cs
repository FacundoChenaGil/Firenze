using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class CrearUsuarioDTO
    {
        public string Nombre_Usuario_Us { get; set; }
        public string Contraseña_Us { get; set; }
        public string? Nombre_Us { get; set; }
        public string? Apellido_Us { get; set; }
        public string? Telefono_Us { get; set; }
        public string Correo_Electronico_Us { get; set; }
        public int Id_Tipo_Usuario_Us { get; set; }
    }
}
