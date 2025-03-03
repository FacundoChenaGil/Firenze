using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CrearTrabajoDTO
    {
        public string Descripcion_Tr { get; set; }
        public decimal Precio_Tr { get; set; }
        public TimeSpan? Duracion_Tr { get; set; }
        public bool Adicional_Tr { get; set; }
        public int Id_Tipo_Trabajo_Tr { get; set; }
    }
}
