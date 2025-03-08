using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace FirenzeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TurnoController : ControllerBase
    {
        private readonly ITurnoService _turnoService;

        public TurnoController(ITurnoService turnoService)
        {
            _turnoService = turnoService;
        }

        [HttpGet("Disponibles")]
        public async Task<IActionResult> ObtenerTurnosDisponibles([FromQuery] DateOnly fechaIngresada)
        {
            var turnosDisponibles = await _turnoService.ObtenerTurnosDisponiblesAsync(fechaIngresada);

            return Ok(turnosDisponibles);
        }

    }
}
