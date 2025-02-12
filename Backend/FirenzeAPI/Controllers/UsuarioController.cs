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

        [HttpGet]
        public async Task<IActionResult> GetAllUsuarios()
        {
            var result = await _usuarioService.GetAllUsuariosAsync();

            return result.Map<IActionResult>(
                onSuccess: listDto => Ok(listDto),
                onFailure: errors => BadRequest(errors)
                );
        }

        [HttpGet("{idUsuario:int}")]
        public async Task<IActionResult> GetUsuarioById(int idUsuario)
        {
            var result = await _usuarioService.GetUsuario(idUsuario);

            return result.Map<IActionResult>(
                onSuccess: usuario => Ok(usuario),
                onFailure: errors => NotFound(errors)
                );
        }

    }
}
