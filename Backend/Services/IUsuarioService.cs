using DB;
using DTOs;
using Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUsuarioService
    {
        public Task<Result<CrearUsuarioDTO>> CrearUsuarioAsync(CrearUsuarioDTO usuarioDTO);
        public Task<Result<List<UsuarioDTO>>> GetAllUsuariosAsync();
        public Task<Result<UsuarioDTO>> GetUsuarioAsync(int idUsuario);
        public Task<bool> ExisteUsuarioAsync(int idUsuario);
        public Task<Result<bool>> ActualizarUsuarioAsync(int idUsuario, ActualizarUsuarioDTO actualizarUsuarioDTO);
        public Task<Result<bool>> EliminarUsuarioAsync(int idUsuario);
    }
}
