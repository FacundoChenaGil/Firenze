using DTOs;
using Microsoft.AspNetCore.Authorization;
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

        [Authorize(Roles = "1")]
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
            var result = await _usuarioService.GetUsuarioAsync(idUsuario);

            return result.Map<IActionResult>(
                onSuccess: usuario => Ok(usuario),
                onFailure: errors => NotFound(errors)
                );
        }

        [HttpPut("{idUsuario:int}")]
        public async Task<IActionResult> ActualizarUsuario(int idUsuario, ActualizarUsuarioDTO actualizarUsuarioDTO)
        {
            var result = await _usuarioService.ActualizarUsuarioAsync(idUsuario, actualizarUsuarioDTO);

            return result.Map<IActionResult>(
                onSuccess: ok => Ok(),
                onFailure: errors => BadRequest(errors)
                );
        }

        [HttpDelete("{idUsuario:int}")]
        public async Task<IActionResult> EliminarUsuario(int idUsuario)
        {
            var result = await _usuarioService.EliminarUsuarioAsync(idUsuario);

            return result.Map<IActionResult>(
                onSuccess: ok => Ok("Se elimino el Usuario con exito."),
                onFailure: errors => NotFound(errors)
                );
        }
    }
}
