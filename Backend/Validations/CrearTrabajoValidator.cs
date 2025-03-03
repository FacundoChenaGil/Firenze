using DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validations
{
    public class CrearTrabajoValidator : AbstractValidator<CrearTrabajoDTO>
    {
        public CrearTrabajoValidator()
        {
            RuleFor(x => x.Descripcion_Tr)
                .NotEmpty().WithMessage("La descripcion es obligatoria.")
                .MaximumLength(100).WithMessage("La descripcion no puede superar los 100 caracteres.");

            RuleFor(x => x.Precio_Tr)
                 .NotEmpty().WithMessage("Debe ingresar un precio.")
                 .LessThanOrEqualTo(0).WithMessage("El precio no puede ser menor que 0.");

            RuleFor(x => x.Adicional_Tr)
                 .NotNull().WithMessage("El campo adicional es requerido.");

            RuleFor(x => x.Id_Tipo_Trabajo_Tr)
                .NotNull().WithMessage("El Tipo de Trabajo debe ser elegido.");
        }
    }
}
