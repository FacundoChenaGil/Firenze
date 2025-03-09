using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class TurnoCalculosResponseDTO
    {
        public decimal Seña { get; set; }
        public TimeSpan? Duracion { get; set; }
        public decimal PrecioTotal { get; set; }
    }
}
