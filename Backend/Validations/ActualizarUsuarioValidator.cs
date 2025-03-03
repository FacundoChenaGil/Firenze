using DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Validations
{
    public class ActualizarUsuarioValidator : AbstractValidator<ActualizarUsuarioDTO>
    {
        public ActualizarUsuarioValidator()
        {
            // Validaciones para ambos tipos de usuario (1- Admin, 2- Cliente)

            RuleFor(x => x.Nombre_Usuario_Us)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio")
                .MaximumLength(20).WithMessage("El nombre de usuario no puede superar los 20 caracteres");

            RuleFor(x => x.Contraseña_Us)
                .NotEmpty().WithMessage("La contraseña es obligatorio")
                .MaximumLength(12).WithMessage("La contraseña no puede superar los 12 caracteres")
                .MinimumLength(8).WithMessage("La contraseña no puede ser menor que 8 caracteres");

            // Validaciones adicionales para los tipos de usuario 2

            RuleFor(x => x.Nombre_Us)
                .NotEmpty().WithMessage("El nombre es obligatorio para el cliente")
                .MaximumLength(20).WithMessage("El nombre no puede superar los 20 caracteres")
                .When(x => x.Id_Tipo_Usuario_Us == 2);

            RuleFor(x => x.Apellido_Us)
                .NotEmpty().WithMessage("El apellido es obligatorio para el cliente")
                .MaximumLength(20).WithMessage("El apellido no puede superar los 20 caracteres")
                .When(x => x.Id_Tipo_Usuario_Us == 2);

            RuleFor(x => x.Telefono_Us)
                    .NotEmpty().WithMessage("El teléfono es obligatorio para el cliente")
                    .MaximumLength(20).WithMessage("El teléfono no puede superar los 20 caracteres")
                    .When(x => x.Id_Tipo_Usuario_Us == 2);
        }
    }
}
