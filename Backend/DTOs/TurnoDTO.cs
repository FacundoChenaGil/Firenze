using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class TurnoDTO
    {
        public int Id_Turno_Tu {  get; set; }
        public DateOnly Fecha_Tu { get; set; }
        public TimeOnly Hora_Tu { get; set; }
        public decimal Seña_Tu { get; set; }
        public TimeSpan? Duracion_Tu { get; set; }
        public decimal Precio_Total_Tu { get; set; }
        public int Id_Usuario_Tu { get; set; }
        public int Id_Estado_Turno_Tu { get; set; }
        public List<string> DescripcionesTrabajo { get; set; }
    }
}
