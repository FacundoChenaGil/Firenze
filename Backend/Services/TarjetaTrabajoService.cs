using DB;
using DTOs;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Validations;

namespace Services
{
    public class TarjetaTrabajoService : ITarjetaTrabajoService
    {
        private readonly FirenzeContext _context;
        private readonly CrearTarjetaTrabajoValidator _crearTarjetaTrabajoValidator;
        private readonly ActualizarTarjetaTrabajoValidator _actualizarTarjetaTrabajoValidator;

        public TarjetaTrabajoService(FirenzeContext context, CrearTarjetaTrabajoValidator crearTarjetaTrabajoValidator, ActualizarTarjetaTrabajoValidator actualizarTrabajoValidator)
        {
            _context = context;
            _crearTarjetaTrabajoValidator = crearTarjetaTrabajoValidator;
            _actualizarTarjetaTrabajoValidator = actualizarTrabajoValidator;
        }

        public async Task<Result<bool>> CrearTarjetaTrabajoAsync(CrearTarjetaTrabajoDTO tarjetaDTO)
        {
            FluentValidation.Results.ValidationResult result = await _crearTarjetaTrabajoValidator.ValidateAsync(tarjetaDTO);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(e => new Error(e.ErrorMessage, e.PropertyName))
                    .ToList();

                return Result<bool>.Failure(errors);
            }


            var tarjeta = new Tarjeta_Trabajo
            {
                Nombre_Tar = tarjetaDTO.Nombre_Tar,
                Descripcion_Tar = tarjetaDTO.Descripcion_Tar,
                Imagen_URL_Tar = tarjetaDTO.Imagen_URL_Tar,
                Id_Trabajo_Tar = tarjetaDTO.Id_Trabajo_Tar
            };

            _context.Add(tarjeta);
            int rowsAffected = await _context.SaveChangesAsync();

            if(rowsAffected == 0)
            {
                return Result<bool>.Failure(new List<Error> { new Error("No se pudo crear la Tarjeta Trabajo.", "CrearTarjetaTrabajoAsync") });
            }

            return Result<bool>.Success(true);
        }

        public async Task<Result<List<TarjetaTrabajoDTO>>> GetAllTarjetasTrabajoAsync()
        {
            var tarjetas = await _context.TarjetasTrabajo
                .Select(tt => new TarjetaTrabajoDTO
                {
                    Id_Tarjeta_Trabajo_Tar = tt.Id_Tarjeta_Trabajo_Tar,
                    Nombre_Tar = tt.Nombre_Tar,
                    Descripcion_Tar = tt.Descripcion_Tar,
                    Imagen_URL_Tar = tt.Imagen_URL_Tar,
                    Id_Trabajo_Tar = tt.Id_Trabajo_Tar
                })
                .ToListAsync();

            if(tarjetas == null || !tarjetas.Any())
            {
                return Result<List<TarjetaTrabajoDTO>>.Failure(new List<Error> { new Error("No se encontraron Tarjetas Trabajo.", "GetAllTarjetasTrabajoAsync") });
            }

            return Result<List<TarjetaTrabajoDTO>>.Success(tarjetas);
        }

        public async Task<Result<TarjetaTrabajoDTO>> GetTarjetaTrabajoAsync(int idTarjetaTrabajo)
        {
            var tarjeta = await _context.TarjetasTrabajo
               .Where(tt => tt.Id_Tarjeta_Trabajo_Tar == idTarjetaTrabajo)
               .Select(tt => new TarjetaTrabajoDTO
               {
                   Id_Tarjeta_Trabajo_Tar = tt.Id_Tarjeta_Trabajo_Tar,
                   Nombre_Tar = tt.Nombre_Tar,
                   Descripcion_Tar = tt.Descripcion_Tar,
                   Imagen_URL_Tar = tt.Imagen_URL_Tar,
                   Id_Trabajo_Tar = tt.Id_Trabajo_Tar
               })
               .FirstOrDefaultAsync();

            if(tarjeta == null)
            {
                return Result<TarjetaTrabajoDTO>.Failure(new List<Error> { new Error($"No se encontro la tarjeta con id: {idTarjetaTrabajo}.", "GetTarjetaTrabajoAsync") });
            }

            return Result<TarjetaTrabajoDTO>.Success(tarjeta);
        }
        public async Task<Result<bool>> ActualizarTarjetaTrabajoAsync(int idTarjetaTrabajo, ActualizarTarjetaTrabajoDTO tarjetaDTO)
        {
            var tarjetaActualizada = await _context.TarjetasTrabajo.FirstOrDefaultAsync(tt => tt.Id_Tarjeta_Trabajo_Tar == idTarjetaTrabajo);
            
            if(tarjetaActualizada == null)
            {
                return Result<bool>.Failure(new List<Error> { new Error($"No se encontro la tarjeta con id: {idTarjetaTrabajo}.", "ActualizarTarjetaTrabajoAsync") });
            }

            FluentValidation.Results.ValidationResult result = await _actualizarTarjetaTrabajoValidator.ValidateAsync(tarjetaDTO);

            if (!result.IsValid)
            {
                var errors = result.Errors
                    .Select(e => new Error(e.ErrorMessage, e.PropertyName))
                    .ToList();

                return Result<bool>.Failure(errors);
            }

            tarjetaActualizada.Nombre_Tar = tarjetaDTO.Nombre_Tar;
            tarjetaActualizada.Descripcion_Tar = tarjetaDTO.Descripcion_Tar;
            tarjetaActualizada.Imagen_URL_Tar = tarjetaDTO.Imagen_URL_Tar;
            tarjetaActualizada.Id_Trabajo_Tar = tarjetaDTO.Id_Trabajo_Tar;

            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> EliminarTarjetaTrabajoAsync(int idTarjetaTrabajo)
        {
            var fila = await _context.TarjetasTrabajo.Where(tt => tt.Id_Tarjeta_Trabajo_Tar == idTarjetaTrabajo).ExecuteDeleteAsync();

            if(fila == 0)
            {
                return Result<bool>.Failure(new List<Error> { new Error($"No se pudo eliminar la tarjeta con id: {idTarjetaTrabajo}.", "EliminarTarjetaTrabajoAsync") });
            }

            return Result<bool>.Success(true);
        }
    }
}
