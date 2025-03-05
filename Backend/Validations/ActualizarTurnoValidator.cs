using DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validations
{
    public class ActualizarTurnoValidator : AbstractValidator<ActualizarTurnoDTO>
    {
        public ActualizarTurnoValidator()
        {
            RuleFor(x => x.Fecha_Tu)
               .NotEmpty().WithMessage("La fecha es obligatoria.")
               .Must(f => f >= DateOnly.FromDateTime(DateTime.Now))
               .WithMessage("La fecha del Turno no puede ser pasada.");

            RuleFor(x => x.Hora_Tu)
                .NotEmpty().WithMessage("La hora es obligatoria.")
                .Must(h => h >= new TimeOnly(8, 0) && h <= new TimeOnly(20, 0))
                .WithMessage("El turno debe ser entre las 08:00 y las 20:00 horas");

            RuleFor(x => x)
            .Custom((turno, context) =>
            {
                DateTime fechaHoraTurno = turno.Fecha_Tu.ToDateTime(turno.Hora_Tu);
                DateTime ahora = DateTime.Now;

                if (fechaHoraTurno < ahora)
                {
                    context.AddFailure("Fecha y Hora", "El turno no puede ser pasado.");
                }
            });

            RuleFor(x => x.Seña_Tu)
                .GreaterThanOrEqualTo(0).WithMessage("La seña no puede ser negativa.");

            RuleFor(x => x.Precio_Total_Tu)
                .GreaterThanOrEqualTo(0).WithMessage("El precio no puede ser negativo.");

            RuleFor(x => x.Id_Usuario_Tu)
                .NotNull().WithMessage("El id de usuario es obligatorio.");

            RuleFor(x => x.Id_Estado_Turno_Tu)
                .NotNull().WithMessage("El id de estado turno es obligatorio.");
        }
    }
}
