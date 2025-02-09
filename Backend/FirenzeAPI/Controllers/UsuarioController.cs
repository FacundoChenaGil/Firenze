using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;

namespace FirenzeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;

        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> CrearUsuario(CrearUsuarioDTO usuarioDTO)
        {
            var result = await _usuarioService.CrearUsuarioAsync(usuarioDTO);

            return result.Map<IActionResult>(
                onSuccess: usuarioDTO => Ok(usuarioDTO),
                onFailure: errors => BadRequest(errors)
                );
        }
    }
}
