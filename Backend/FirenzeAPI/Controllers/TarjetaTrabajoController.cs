using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Services;

namespace FirenzeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaTrabajoController : ControllerBase
    {
        private readonly ITarjetaTrabajoService _tarjetaTrabajoService;

        public TarjetaTrabajoController(ITarjetaTrabajoService tarjetaTrabajoService)
        {
            _tarjetaTrabajoService = tarjetaTrabajoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTarjetaTrabajo(CrearTarjetaTrabajoDTO tarjetaDTO)
        {
            var result = await _tarjetaTrabajoService.CrearTarjetaTrabajoAsync(tarjetaDTO);

            return result.Map<IActionResult>(
                onSuccess: ok => Ok(ok),
                onFailure: error => BadRequest(error)
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTarjetasTrabajo()
        {
            var result = await _tarjetaTrabajoService.GetAllTarjetasTrabajoAsync();

            return result.Map<IActionResult>(
                onSuccess: tarjetas => Ok(tarjetas),
                onFailure: error => NotFound(error)
                );
        }

        [HttpGet("{idTarjetaTrabajo:int}")]
        public async Task<IActionResult> GetTarjetaTrabajo(int idTarjetaTrabajo)
        {
            var result = await _tarjetaTrabajoService.GetTarjetaTrabajoAsync(idTarjetaTrabajo);

            return result.Map<IActionResult>(
                onSuccess: tarjeta => Ok(tarjeta),
                onFailure: error => NotFound(error)
                );
        }

        [HttpPut("{idTarjetaTrabajo:int}")]
        public async Task<IActionResult> ActualizarTarjetaTrabajo(int idTarjetaTrabajo, ActualizarTarjetaTrabajoDTO tarjetaDTO)
        {
            var result = await _tarjetaTrabajoService.ActualizarTarjetaTrabajoAsync(idTarjetaTrabajo, tarjetaDTO);

            return result.Map<IActionResult>(
                onSuccess: ok => Ok(ok),
                onFailure: error => BadRequest(error)
                );
        }

        [HttpDelete("{idTarjetaTrabajo:int}")]
        public async Task<IActionResult> EliminarTarjetaTrabajo(int idTarjetaTrabajo)
        {
            var result = await _tarjetaTrabajoService.EliminarTarjetaTrabajoAsync(idTarjetaTrabajo);

            return result.Map<IActionResult>(
                onSuccess: ok => Ok(ok),
                onFailure: error => BadRequest(error)
                );
        }
    }
}
