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
        public Task<Result<UsuarioDTO>> GetUsuario(int idUsuario);

    }
}
