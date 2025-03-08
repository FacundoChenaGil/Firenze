using DB;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;

namespace Services
{
    public class TipoTrabajoService : ITipoTrabajoService
    {
        private readonly FirenzeContext _context;

        public TipoTrabajoService(FirenzeContext context)
        {
            _context = context;
        }

        public async Task<Result<List<TipoTrabajoDTO>>> GetAllTiposTrabajoAsync()
        {
            var tiposTrabajo = await _context.TipoTrabajos
                .Select(tt => new TipoTrabajoDTO
                {
                    Id_Tipo_Trabajo_Ttr = tt.Id_Tipo_Trabajo_Ttr,
                    Descripcion_Ttr = tt.Descripcion_Ttr
                })
                .ToListAsync();

            if(tiposTrabajo == null || !tiposTrabajo.Any())
            {
                return Result<List<TipoTrabajoDTO>>.Failure(new List<Error> { new Error("No se encontraron Tipos Trabajo.", "GetAllTiposTrabajoAsync") });
            }

            return Result<List<TipoTrabajoDTO>>.Success(tiposTrabajo);
        }
    }
}
