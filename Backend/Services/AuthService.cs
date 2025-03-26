using DB;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Validations;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        public AuthService(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }
        public async Task<Result<CrearUsuarioDTO>> RegistrarAsync(CrearUsuarioDTO usuarioDTO)
        {
            return await _usuarioService.CrearUsuarioAsync(usuarioDTO);
        }

        private string GenerateToken(Usuario usuario)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, usuario.Nombre_Usuario_Us!),
                new Claim(ClaimTypes.Role, usuario.Id_Tipo_Usuario_Us.ToString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _configuration.GetValue<string>("Appsettings:Issuer"),
                audience: _configuration.GetValue<string>("Appsettings:Audience"),
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }


        public async Task<Result<string>> LoginAsync(LoginDTO loginDTO)
        {
            Usuario usuario = new Usuario();

            usuario = await _usuarioService.GetUsuarioAsync(loginDTO.Nombre_Usuario);

            if(usuario == null)
            {
                return Result<string>.Failure(new List<Error> { new Error("El usuario ingresado no existe", "LoginAsync") });
            }

            if(!Hasher.VerifyPassword(loginDTO.Contraseña, usuario.Contraseña_Us))
            {
                return Result<string>.Failure(new List<Error> { new Error("La contraseña no es la correcta.", "LoginAync") });
            }

            string token = GenerateToken(usuario);

            return Result<string>.Success(token);
        }
    }
}
