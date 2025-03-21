using DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validations
{
    public class CrearTarjetaTrabajoValidator : AbstractValidator<CrearTarjetaTrabajoDTO>
    {
        public CrearTarjetaTrabajoValidator()
        {
            RuleFor(x => x.Nombre_Tar)
                .NotEmpty().WithMessage("El nombre es obligatorio.")
                .MaximumLength(20).WithMessage("El nombre no puede superar los 20 caracteres");

            RuleFor(x => x.Descripcion_Tar)
                .NotEmpty().WithMessage("La descripcion es obligatoria.")
                .MaximumLength(100).WithMessage("La descripcion no puede superar los 100 caracteres.");

            RuleFor(x => x.Imagen_URL_Tar)
                .NotEmpty().WithMessage("La URL es obligatoria");

            RuleFor(x => x.Id_Trabajo_Tar)
                .NotNull().WithMessage("Debe asociar un Trabajo a la Tarjeta");
        }
    }
}
