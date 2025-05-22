using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CrearPreferenciaDTO
    {
        public int IdUsuario {  get; set; }
        public DateOnly FechaTurno { get; set; }
        public TimeOnly HoraTurno { get; set; }
        public TimeSpan? DuracionTurno { get; set; }
        public decimal Seña { get; set; }
        public decimal PrecioTotal { get; set; }
        public bool AbonaTotal { get; set; }
        public string Descripcion { get; set; }
        public string NombreCliente { get; set; }
        public string CorreoElectronico { get; set; }
        public string CodigoArea { get; set; }
        public string Telefono { get; set; }
        public List<int> IdsTrabajo { get; set; }

    }
}
