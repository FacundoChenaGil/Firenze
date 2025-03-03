using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace FirenzeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrabajoController : ControllerBase
    {
        private readonly ITrabajoService _trabajoService;

        public TrabajoController(ITrabajoService trabajoService)
        {
            _trabajoService = trabajoService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearTrabajo(CrearTrabajoDTO trabajoDTO)
        {
            var result = await _trabajoService.CrearTrabajoAsync(trabajoDTO);

            return result.Map<IActionResult>(
                onSuccess: trabajoDTO => Ok(trabajoDTO),
                onFailure: error => BadRequest(error)
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTrabajos()
        {
            var result = await _trabajoService.GetAllTrabajosAsync();

            return result.Map<IActionResult>(
                onSuccess: trabajosDTO => Ok(trabajosDTO),
                onFailure: error => NotFound(error)
                );
        }

        [HttpGet("{idTrabajo:int}")]
        public async Task<IActionResult> GetTrabajo(int idTrabajo)
        {
            var result = await _trabajoService.GetTrabajoAsync(idTrabajo);

            return result.Map<IActionResult>(
                onSuccess: trabajoDTO => Ok(trabajoDTO),
                onFailure: error => NotFound(error)
                );

        }

        [HttpPut("{idTrabajo:int}")]
        public async Task<IActionResult> ActualizarTrabajo(int idTrabajo, ActualizarTrabajoDTO trabajoDTO)
        {
            var result = await _trabajoService.ActualizarTrabajoAsync(idTrabajo, trabajoDTO);

            return result.Map<IActionResult>(
                onSuccess: ok => Ok(ok),
                onFailure: error => BadRequest(error)
                );
        }

        [HttpDelete("{idTrabajo:int}")]
        public async Task<IActionResult> EliminarTrabajo(int idTrabajo)
        {
            var result = await _trabajoService.EliminarTrabajoAsync(idTrabajo);

            return result.Map<IActionResult>(
                onSuccess: ok => Ok(ok),
                onFailure: error => BadRequest(error)
                );
        }
    }
}
