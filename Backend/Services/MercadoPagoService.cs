using DTOs;
using MercadoPago.Client.Common;
using MercadoPago.Client.Preference;
using MercadoPago.Config;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using static System.Net.WebRequestMethods;

namespace Services
{
    public class MercadoPagoService : IMercadoPagoService
    {
        private readonly IConfiguration _confguration;

        public MercadoPagoService(IConfiguration confguration)
        {
            _confguration = confguration;
        }

        public async Task<Result<string>> CrearPreferenciaAsync(CrearPreferenciaDTO preferenciaDTO)
        {
            var accessToken = getAccessToken();
            var publicKey = getPublicKey();

            MercadoPagoConfig.AccessToken = accessToken;

            var preferenceCliente = new PreferenceClient();

            decimal montoAPagar = 0;
            string titulo;
            
            if(preferenciaDTO.AbonaTotal)
            {
                montoAPagar = preferenciaDTO.PrecioTotal;
                titulo = "Total turno";
            }
            else
            {
                montoAPagar = preferenciaDTO.Seña;
                titulo = "Seña turno";
            }

                var items = new List<PreferenceItemRequest>
            {
                new PreferenceItemRequest
                {
                    Title = titulo,
                    Description = preferenciaDTO.Descripcion,
                    Quantity = 1,
                    CurrencyId = "ARS",
                    UnitPrice = montoAPagar
                }
            };

            var request = new PreferenceRequest
            {
                Items = items,
                Payer = new PreferencePayerRequest
                {
                    Name = preferenciaDTO.NombreCliente,
                    Email = preferenciaDTO.CorreoElectronico,
                    Phone = new PhoneRequest
                    {
                        AreaCode = preferenciaDTO.CodigoArea,
                        Number = preferenciaDTO.Telefono
                    }

                },
                BackUrls = new PreferenceBackUrlsRequest
                {
                    Success = "https://localhost:7053/success",
                    Failure = "https://localhost:7053/failure"
                },
                AutoReturn = "approved",
                PaymentMethods = new PreferencePaymentMethodsRequest
                {
                    ExcludedPaymentTypes = new List<PreferencePaymentTypeRequest>
                    {
                        new PreferencePaymentTypeRequest { Id = "ticket"},
                        new PreferencePaymentTypeRequest { Id = "atm" }

                    }
                },
                Metadata = new Dictionary<string, object>
                {
                    { "idUsuario", preferenciaDTO.IdUsuario },
                    { "fecha", preferenciaDTO.FechaTurno.ToString() },
                    { "hora", preferenciaDTO.HoraTurno.ToString() },
                    { "duracion", preferenciaDTO.DuracionTurno?.ToString() },
                    { "idsTrabajo", string.Join(",", preferenciaDTO.IdsTrabajo) }
                }


            };

            var preference = await preferenceCliente.CreateAsync(request);

            if(preference == null)
            {
                return Result<string>.Failure(new List<Error> { new Error("No se pudo crear la preferencia", "CrearPreferenciaAsync") });
            }


            return Result<string>.Success(preference.InitPoint);
            
        }

        private string getAccessToken()
        {
            var token = _confguration.GetValue<string>("MercadoPago:AccesToken");
            return token.ToString() ?? string.Empty;
        }

        private string getPublicKey()
        {
            var key = _confguration.GetValue<string>("MercadoPago:PublicKey");
            return key.ToString() ?? string.Empty;
        }

    }
}
