using FluentValidation;
using IzumuClientes.Application.DTOs;

namespace IzumuClientes.API.Validators
{
    public class ClientValidator : AbstractValidator<ClientRequestDTO>
    {
        public ClientValidator()
        {
            RuleFor(x => x.TipoIdentificacion)
                .GreaterThan(0)
                .WithMessage("El tipo de identificación es obligatorio.");

            RuleFor(x => x.Identificacion)
                .NotEmpty()
                .WithMessage("La identificación es obligatoria.")
                .MaximumLength(20)
                .WithMessage("La identificación no puede superar 20 caracteres.")
                .Matches(@"^\d+$")
                .WithMessage("La identificación solo debe contener números.");

            RuleFor(x => x.PrimerNombre)
                .NotEmpty()
                .WithMessage("El primer nombre es obligatorio.")
                .MaximumLength(50)
                .WithMessage("El primer nombre no puede superar 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")
                .WithMessage("El primer nombre solo debe contener letras.");

            RuleFor(x => x.SegundoNombre)
                .MaximumLength(50)
                .WithMessage("El segundo nombre no puede superar 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")
                .WithMessage("El segundo nombre solo debe contener letras.")
                .When(x => !string.IsNullOrEmpty(x.SegundoNombre));

            RuleFor(x => x.PrimerApellido)
                .NotEmpty()
                .WithMessage("El primer apellido es obligatorio.")
                .MaximumLength(50)
                .WithMessage("El primer apellido no puede superar 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")
                .WithMessage("El primer apellido solo debe contener letras.");

            RuleFor(x => x.SegundoApellido)
                .MaximumLength(50)
                .WithMessage("El segundo apellido no puede superar 50 caracteres.")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$")
                .WithMessage("El segundo apellido solo debe contener letras.")
                .When(x => !string.IsNullOrEmpty(x.SegundoApellido));

            RuleFor(x => x.FechaNacimiento)
                .NotEmpty()
                .WithMessage("La fecha de nacimiento es obligatoria.")
                .Matches(@"^\d{2}-\d{2}-\d{4}$")
                .WithMessage("El formato de la fecha debe ser dd-MM-yyyy.")
                .Must(FechaValida)
                .WithMessage("La fecha de nacimiento no es válida.")
                .Must(MayorDeEdad)
                .WithMessage("El cliente debe ser mayor de edad.");

            RuleFor(x => x.IDPlan)
                .GreaterThan(0)
                .WithMessage("El plan es obligatorio.");
        }

        private bool FechaValida(string fecha)
        {
            return DateTime.TryParseExact(
                fecha,
                "dd-MM-yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out _
            );
        }

        private bool MayorDeEdad(string fecha)
        {
            if (!DateTime.TryParseExact(
                fecha,
                "dd-MM-yyyy",
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None,
                out DateTime fechaNacimiento))
                return false;

            var edad = DateTime.Today.Year - fechaNacimiento.Year;
            if (fechaNacimiento.Date > DateTime.Today.AddYears(-edad)) edad--;
            return edad >= 18;
        }
    }
}
