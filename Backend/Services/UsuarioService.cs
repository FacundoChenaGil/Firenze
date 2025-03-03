using DB;
using DTOs;
using Utilities;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;
using Validations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Globalization;

namespace Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly FirenzeContext _context;
        private readonly CrearUsuarioValidator _crearUsuarioValidator;
        private readonly ActualizarUsuarioValidator _actualizarUsuarioValidator;

        public UsuarioService(FirenzeContext context, CrearUsuarioValidator crearUsuarioValidator, ActualizarUsuarioValidator actualizarUsuarioValidator)
        {
            _context = context;
            _crearUsuarioValidator = crearUsuarioValidator;
            _actualizarUsuarioValidator = actualizarUsuarioValidator;
        }

        private async Task<bool> ExisteCorreoAsync(string correoElectronico)
        {
            return await _context.Usuarios.AnyAsync(u => u.Correo_Electronico_Us == correoElectronico);
        }

        private async Task<bool> ExisteNombreUsuarioAsync(int idUsuario, string nombreUsuario)
        {
            return await _context.Usuarios.AnyAsync(u => u.Id_Usuario_Us != idUsuario && u.Nombre_Usuario_Us == nombreUsuario);
        }
        private async Task<bool> ExisteNombreUsuarioAsync(string nombreUsuario)
        {
            return await _context.Usuarios.AnyAsync(u => u.Nombre_Usuario_Us == nombreUsuario);
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

            if(await ExisteNombreUsuarioAsync(usuarioDTO.Nombre_Usuario_Us))
            {
                return Result<CrearUsuarioDTO>.Failure(new List<Error> { new Error("El usuario ingresado ya existe.", "CrearUsuarioAsync") });
            }

            if (await ExisteCorreoAsync(usuarioDTO.Correo_Electronico_Us))
            {
                return Result<CrearUsuarioDTO>.Failure(new List<Error> { new Error("El correo electronico ingresado ya se registro.", "CrearUsuarioAsync") });
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
                return Result<CrearUsuarioDTO>.Failure(new List<Error> { new Error("No se pudo crear el usuario", "CrearUsuarioAsync") });
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
                return Result<List<UsuarioDTO>>.Failure(new List<Error> { new Error("No se encontraron usuarios", "GetAllUsuariosAsync") });
            }
          
            return Result<List<UsuarioDTO>>.Success(usuarios);
        }

        public async Task<Result<UsuarioDTO>> GetUsuarioAsync(int idUsuario)
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
                return Result<UsuarioDTO>.Failure(new List<Error> { new Error($"No se encontro el usuario con ID: {idUsuario}.", "GetUsuarioAsync") });
            }

            return Result<UsuarioDTO>.Success(usuario);
        }

        public async Task<Usuario> GetUsuarioAsync(string nombreUsuario)
        {
            var usuario = await _context.Usuarios
                .Where(u => u.Nombre_Usuario_Us == nombreUsuario)
                .Select(u => new Usuario
                {
                    Id_Usuario_Us = u.Id_Usuario_Us,
                    Nombre_Usuario_Us = u.Nombre_Usuario_Us,
                    Contraseña_Us = u.Contraseña_Us,
                    Nombre_Us = u.Nombre_Us,
                    Apellido_Us = u.Apellido_Us,
                    Correo_Electronico_Us = u.Correo_Electronico_Us,
                    Telefono_Us = u.Telefono_Us,
                    Id_Tipo_Usuario_Us = u.Id_Tipo_Usuario_Us
                })
                .FirstOrDefaultAsync();

            return usuario!;
        }

        public async Task<Result<bool>> ActualizarUsuarioAsync(int idUsuario, ActualizarUsuarioDTO actualizarUsuarioDTO)
        {
            var usuarioActualizado = await _context.Usuarios.FirstOrDefaultAsync(u => u.Id_Usuario_Us == idUsuario);

            if(usuarioActualizado == null)
            {
                return Result<bool>.Failure(new List<Error> { new Error($"No se encontro el usuario con ID: {idUsuario}.", "EliminarUsuarioAsync") });
            }

            FluentValidation.Results.ValidationResult result = await _actualizarUsuarioValidator.ValidateAsync(actualizarUsuarioDTO);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(e => new Error(e.ErrorMessage, e.PropertyName))
                    .ToList();

                return Result<bool>.Failure(errors);
            }

            if (await ExisteNombreUsuarioAsync(actualizarUsuarioDTO.Id_Usuario_Us ,actualizarUsuarioDTO.Nombre_Usuario_Us))
            {
                return Result<bool>.Failure(new List<Error> { new Error("El usuario ingresado ya existe.", "ActualizarUsuarioAsync") });
            }

            if (!string.IsNullOrWhiteSpace(actualizarUsuarioDTO.Contraseña_Us))
            {
                usuarioActualizado.Contraseña_Us = Hasher.HashPassword(actualizarUsuarioDTO.Contraseña_Us);
            }

            usuarioActualizado.Nombre_Usuario_Us = actualizarUsuarioDTO.Nombre_Usuario_Us;
            usuarioActualizado.Nombre_Us = actualizarUsuarioDTO.Nombre_Us;
            usuarioActualizado.Apellido_Us = actualizarUsuarioDTO.Apellido_Us;
            usuarioActualizado.Telefono_Us = actualizarUsuarioDTO.Telefono_Us;

            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> EliminarUsuarioAsync(int idUsuario)
        {
            var fila = await _context.Usuarios.Where(u => u.Id_Usuario_Us == idUsuario).ExecuteDeleteAsync();

            if (fila == 0)
            {
                return Result<bool>.Failure(new List<Error> { new Error($"No se encontro el usuario con ID: {idUsuario}.", "EliminarUsuarioAsync") });
            }

            return Result<bool>.Success(true);
        }

    }
}
