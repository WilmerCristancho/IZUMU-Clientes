using FluentValidation;
using IzumuClientes.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.Validators
{
    public class PhoneValidator : AbstractValidator<PhoneRequestDTO>
    {
        public PhoneValidator()
        {
            RuleFor(x => x.IDCliente)
                .GreaterThan(0)
                .WithMessage("El cliente es obligatorio.");

            RuleFor(x => x.Telefono)
                .NotEmpty()
                .WithMessage("El número de teléfono es obligatorio.")
                .MaximumLength(10)
                .WithMessage("El teléfono no puede superar 10 caracteres.")
                .Matches(@"^\d+$")
                .WithMessage("El teléfono solo debe contener números.");
        }
    }
}
