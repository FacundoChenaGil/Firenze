using DB;
using DTOs;
using Utilities;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Validations;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly FirenzeContext _context;
        private readonly CrearUsuarioValidator _crearUsuarioValidator;

        public UsuarioService(FirenzeContext context, CrearUsuarioValidator crearUsuarioValidator)
        {
            _context = context;
            _crearUsuarioValidator = crearUsuarioValidator;
        }
        public async Task<Result<CrearUsuarioDTO>> CrearUsuarioAsync(CrearUsuarioDTO usuarioDTO)
        {
            // Se escribe "FluentValidation.Results" para no chocar con la ambiguedad de las DataAnnotations de ASP.

            FluentValidation.Results.ValidationResult result = await _crearUsuarioValidator.ValidateAsync(usuarioDTO);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(e => new Error(e.ErrorMessage, e.PropertyName))
                    .ToList();

                return Result<CrearUsuarioDTO>.Failure(errors);
            }

            var usuario = new Usuario
            {
                Nombre_Usuario_Us = usuarioDTO.Nombre_Usuario_Us,
                Contraseña_Us = Hasher.HashPassword(usuarioDTO.Contraseña_Us),
                Nombre_Us = usuarioDTO.Nombre_Us,
                Apellido_Us = usuarioDTO.Apellido_Us,
                Telefono_Us = usuarioDTO.Telefono_Us,
                Correo_Electronico_Us = usuarioDTO.Correo_Electronico_Us,
                Id_Tipo_Usuario_Us = usuarioDTO.Id_Tipo_Usuario_Us
            };

            _context.Add(usuario);
            int rowsAffected = await _context.SaveChangesAsync();

            if(rowsAffected == 0)
            {
                return Result<CrearUsuarioDTO>.Failure(new List<Error> { new Error("No se pudo crear el usuario", "FirenzeBD") });
            }


            return Result<CrearUsuarioDTO>.Success(usuarioDTO);

        }

        public async Task<Result<List<UsuarioDTO>>> GetAllUsuariosAsync()
        {
                var usuarios = await _context.Usuarios
            .Select(u => new UsuarioDTO
            {
                Id_Usuario_Us = u.Id_Usuario_Us,
                Nombre_Usuario_Us = u.Nombre_Usuario_Us,
                Nombre_Us = u.Nombre_Us,
                Apellido_Us = u.Apellido_Us,
                Correo_Electronico_Us = u.Correo_Electronico_Us,
                Telefono_Us = u.Telefono_Us
            })
            .ToListAsync();

            if(usuarios == null || !usuarios.Any())
            {
                return Result<List<UsuarioDTO>>.Failure(new List<Error> { new Error("No se encontraron usuarios", "Usuarios") });
            }
          
            return Result<List<UsuarioDTO>>.Success(usuarios);
        }

        public async Task<Result<UsuarioDTO>> GetUsuario(int idUsuario)
        {
            var usuario =  await _context.Usuarios
                .Where(u => u.Id_Usuario_Us == idUsuario)
                .Select(u => new UsuarioDTO
                {
                    Id_Usuario_Us = u.Id_Usuario_Us,
                    Nombre_Usuario_Us = u.Nombre_Usuario_Us,
                    Nombre_Us = u.Nombre_Us,
                    Apellido_Us = u.Apellido_Us,
                    Correo_Electronico_Us = u.Correo_Electronico_Us,
                    Telefono_Us = u.Telefono_Us
                })
                .FirstOrDefaultAsync();

            if(usuario == null)
            {
                return Result<UsuarioDTO>.Failure(new List<Error> { new Error($"No se encontro el usuario con ID: {idUsuario}.", "Usuarios") });
            }

            return Result<UsuarioDTO>.Success(usuario);
        }
    }
}
