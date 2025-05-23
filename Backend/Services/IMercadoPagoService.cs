using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services
{
    public interface IMercadoPagoService
    {
        public Task<Result<string>> CrearPreferenciaAsync(CrearPreferenciaDTO preferenciaDTO);

    }
}
