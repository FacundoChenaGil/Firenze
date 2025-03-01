using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services
{
    public interface IAuthService
    {
        public Task<Result<CrearUsuarioDTO>> RegistrarAsync(CrearUsuarioDTO crearUsuarioDTO);
        public Task<Result<string>> LoginAsync(LoginDTO loginDTO);
    }
}
