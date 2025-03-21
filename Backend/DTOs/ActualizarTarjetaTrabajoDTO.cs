using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class ActualizarTarjetaTrabajoDTO
    {
        public int Id_Tarjeta_Trabajo_Tar { get; set; }
        public string Nombre_Tar { get; set; }
        public string Descripcion_Tar { get; set; }
        public string Imagen_URL_Tar { get; set; }
        public int Id_Trabajo_Tar { get; set; }
    }
}
