using DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Services;

namespace FirenzeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
        {
            var result = await _authService.LoginAsync(loginDTO);

            return result.Map<IActionResult>(
                onSuccess: token => Ok(token),
                onFailure: errors => NotFound(errors)
                );
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar(CrearUsuarioDTO usuarioDTO)
        {
            var result = await _authService.RegistrarAsync(usuarioDTO);

            return result.Map<IActionResult>(
                onSuccess: usuario => Ok(usuarioDTO),
                onFailure: errors => NotFound(errors)
                );
        }


    }
}
