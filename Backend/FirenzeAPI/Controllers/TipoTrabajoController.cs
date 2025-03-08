using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace FirenzeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TipoTrabajoController : ControllerBase
    {
        private readonly ITipoTrabajoService _tipoTrabajoService;

        public TipoTrabajoController(ITipoTrabajoService tipoTrabajoService)
        {
            _tipoTrabajoService = tipoTrabajoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTiposTrabajo()
        {
            var result = await _tipoTrabajoService.GetAllTiposTrabajoAsync();

            return result.Map<IActionResult>(
                onSuccess: tiposTrabajo => Ok(tiposTrabajo),
                onFailure: error => NotFound(error)
                );
        }

    }
}
