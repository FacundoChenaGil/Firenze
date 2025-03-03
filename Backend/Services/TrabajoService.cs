using DB;
using DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Utilities;
using Validations;

namespace Services
{
    public class TrabajoService : ITrabajoService
    {

        private readonly FirenzeContext _context;
        private readonly CrearTrabajoValidator _crearTrabajoValidator;
        private readonly ActualizarTrabajoValidator _actualizarTrabajoValidator;

        public TrabajoService(FirenzeContext context, CrearTrabajoValidator crearTrabajoValidator, ActualizarTrabajoValidator actualizarTrabajoValidator)
        {
            _context = context;
            _crearTrabajoValidator = crearTrabajoValidator;
            _actualizarTrabajoValidator = actualizarTrabajoValidator;
        }

        public async Task<Result<CrearTrabajoDTO>> CrearTrabajoAsync(CrearTrabajoDTO trabajoDTO)
        {
            FluentValidation.Results.ValidationResult result = await _crearTrabajoValidator.ValidateAsync(trabajoDTO);

            if(!result.IsValid)
            {
                var errors = result.Errors
                    .Select(e => new Error(e.ErrorMessage, e.PropertyName))
                    .ToList();

                return Result<CrearTrabajoDTO>.Failure(errors);
            }

            var trabajo = new Trabajo
            {
                Descripcion_Tr = trabajoDTO.Descripcion_Tr,
                Precio_Tr = trabajoDTO.Precio_Tr,
                Duracion_Tr = trabajoDTO.Duracion_Tr,
                Adicional_Tr = trabajoDTO.Adicional_Tr,
                Id_Tipo_Trabajo_Tr = trabajoDTO.Id_Tipo_Trabajo_Tr
            };

            _context.Add(trabajo);

            int rowsAffected = await _context.SaveChangesAsync();

            if(rowsAffected == 0)
            {
                return Result<CrearTrabajoDTO>.Failure(new List<Error> { new Error("No se pudo crear el trabajo.", "CrearTrabajoAsync") });
            }

            return Result<CrearTrabajoDTO>.Success(trabajoDTO);
        }

        public async Task<Result<List<TrabajoDTO>>> GetAllTrabajosAsync()
        {
            var trabajos = await _context.Trabajos
                .Select(t => new TrabajoDTO
                {
                    Id_Trabajo_Tr = t.Id_Trabajo_Tr,
                    Descripcion_Tr = t.Descripcion_Tr,
                    Precio_Tr = t.Precio_Tr,
                    Duracion_Tr = t.Duracion_Tr,
                    Adicional_Tr = t.Adicional_Tr,
                    Id_Tipo_Trabajo_Tr = t.Id_Tipo_Trabajo_Tr
                })
                .ToListAsync();

            if(trabajos == null || !trabajos.Any())
            { 
                return Result<List<TrabajoDTO>>.Failure(new List<Error> { new Error("No se encontraron trabajos.", "GetAllTrabajosAsync") });
            }

            return Result<List<TrabajoDTO>>.Success(trabajos);
        }

        public async Task<Result<TrabajoDTO>> GetTrabajoAsync(int idTrabajo)
        {
            var trabajo = await _context.Trabajos
                .Where(t => t.Id_Trabajo_Tr == idTrabajo)
                .Select(t => new TrabajoDTO
                {
                    Id_Trabajo_Tr = t.Id_Trabajo_Tr,
                    Descripcion_Tr = t.Descripcion_Tr,
                    Precio_Tr = t.Precio_Tr,
                    Duracion_Tr = t.Duracion_Tr,
                    Adicional_Tr = t.Adicional_Tr,
                    Id_Tipo_Trabajo_Tr = t.Id_Tipo_Trabajo_Tr
                })
                .FirstOrDefaultAsync();

            if (trabajo == null)
            {
                return Result<TrabajoDTO>.Failure(new List<Error> { new Error($"No se encontro el trabajo con id: {idTrabajo}.", "GetUsuarioAsync") });
            }

            return Result<TrabajoDTO>.Success(trabajo);
        }

        public async Task<Result<bool>> ActualizarTrabajoAsync(int idTrabajo, ActualizarTrabajoDTO trabajoDTO)
        {
            var trabajoActualizado = await _context.Trabajos.FirstOrDefaultAsync(t => t.Id_Trabajo_Tr == idTrabajo);

            if(trabajoActualizado == null)
            {
                return Result<bool>.Failure(new List<Error> { new Error($"No se encontro el trabajo con id: {idTrabajo}.", "ActualizarTrabajoAsync") });
            }

            FluentValidation.Results.ValidationResult result = await _actualizarTrabajoValidator.ValidateAsync(trabajoDTO);

            if(!result.IsValid)
            {
                var errors = result.Errors
                    .Select(e => new Error(e.ErrorMessage, e.PropertyName))
                    .ToList();

                return Result<bool>.Failure(errors);
            }

            trabajoActualizado.Descripcion_Tr = trabajoDTO.Descripcion_Tr;
            trabajoActualizado.Precio_Tr = trabajoDTO.Precio_Tr;
            trabajoActualizado.Duracion_Tr = trabajoDTO.Duracion_Tr;
            trabajoActualizado.Adicional_Tr = trabajoDTO.Adicional_Tr;
            trabajoActualizado.Id_Tipo_Trabajo_Tr = trabajoDTO.Id_Tipo_Trabajo_Tr;

            await _context.SaveChangesAsync();

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> EliminarTrabajoAsync(int idTrabajo)
        {
            var fila = await _context.Trabajos.Where(t => t.Id_Trabajo_Tr == idTrabajo).ExecuteDeleteAsync();

            if(fila == 0)
            {
                return Result<bool>.Failure(new List<Error> { new Error($"No se pudo eliminar el trabajo con id: {idTrabajo}.", "EliminarTrabajoAsync") });
            }

            return Result<bool>.Success(true);
        }
    } 
}
