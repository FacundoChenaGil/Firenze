using DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace FirenzeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MercadoPagoController : ControllerBase
    {
        private readonly IMercadoPagoService _mercadoPagoService;

        public MercadoPagoController(IMercadoPagoService mercadoPagoService)
        {
            _mercadoPagoService = mercadoPagoService;
        }

        [HttpPost("CrearPreferencia")]
        public async Task<IActionResult> CrearPreferencia(CrearPreferenciaDTO preferenciaDTO)
        {
            var result = await _mercadoPagoService.CrearPreferenciaAsync(preferenciaDTO);

            return result.Map<IActionResult>(
                onSuccess: url => Ok(url),
                onFailure: error => BadRequest(error)
                );
        }

        [HttpGet("ping")]
        public IActionResult Ping() => Ok("pong");

    }
}
