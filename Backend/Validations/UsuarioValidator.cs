using DTOs;
using FluentValidation;

namespace Validations
{
    public class UsuarioValidator : AbstractValidator<UsuarioDTO>
    {
        public UsuarioValidator()
        {
            RuleFor(x => x.Nombre_Usuario_Us)
                .NotEmpty().WithMessage("El nombre de usuario es obligatorio")
                .MaximumLength(20).WithMessage("El nombre de usuario no puede superar los 20 caracteres");

            RuleFor(x => x.Contraseña_Us)
                .NotEmpty().WithMessage("La contraseña es obligatorio")
                .MaximumLength(20).WithMessage("La contraseña no puede superar los 12 caracteres")
                .MinimumLength(8).WithMessage("La contraseña no puede ser menor que 8 caracteres");

            RuleFor(x => x.Nombre_Us)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(20).WithMessage("El nombre no puede superar los 20 caracteres")
                .When(x => x.Id_Tipo_Usuario_Us == 2);

            RuleFor(x => x.Apellido_Us)
                .NotEmpty().WithMessage("El apellido es obligatorio")
                .MaximumLength(20).WithMessage("El apellido no puede superar los 20 caracteres")
                .When(x => x.Id_Tipo_Usuario_Us == 2);

            RuleFor(x => x.Telefono_Us)
                .NotEmpty().WithMessage("El numero de telefono es obligatorio")
                .MaximumLength(20).WithMessage("El numero de telefono no puede superar los 20 caracteres")
                .When(x => x.Id_Tipo_Usuario_Us == 2);

            RuleFor(x => x.Apellido_Us)
                .NotEmpty().WithMessage("El email es obligatorio")
                .MaximumLength(20).WithMessage("El email no puede superar los 20 caracteres")
                .EmailAddress().WithMessage("Debe ser un email válido");
        }
    }
}
