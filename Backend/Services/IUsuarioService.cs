using DB;
using DTOs;
using Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IUsuarioService
    {
        public Task<Result<Usuario>> CrearUsuarioAsync(CrearUsuarioDTO usuarioDTO);

    }
}
