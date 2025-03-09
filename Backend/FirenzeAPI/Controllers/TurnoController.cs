using DTOs;
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

        [HttpPost("CalculoTurno")]
        public async Task<IActionResult> CalcularTurno(List<int> idsTrabajo)
        {
            var result = await _turnoService.CalcularTurnoAsync(idsTrabajo);

            return result.Map<IActionResult>(
                onSuccess: calculoDTO => Ok(calculoDTO),
                onFailure: error => BadRequest(error)
                );
        }

        [HttpPost("CrearTurno")]
        public async Task<IActionResult> CrearTurno(CrearTurnoDTO turnoDTO)
        {
            var result = await _turnoService.CrearTurnoAsync(turnoDTO);

            return result.Map<IActionResult>(
                onSuccess: ok => Ok(ok),
                onFailure: error => BadRequest(error)
                );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTurnos()
        {
            var result = await _turnoService.GetAllTurnosAsync();

            return result.Map<IActionResult>(
                onSuccess: turnos => Ok(turnos),
                onFailure: error => NotFound(error)
                );
        }

        [HttpGet("{idTurno:int}")]
        public async Task<IActionResult> GetTurno(int idTurno)
        {
            var result = await _turnoService.GetTurnoAsync(idTurno);

            return result.Map<IActionResult>(
                onSuccess: turno => Ok(turno),
                onFailure: error => NotFound(error)
                );
        }

    }
}
