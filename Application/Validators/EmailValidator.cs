using FluentValidation;
using IzumuClientes.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.Validators
{
    public class EmailValidator : AbstractValidator<EmailRequestDTO>
    {
        public EmailValidator()
        {
            RuleFor(x => x.IDCliente)
                .GreaterThan(0)
                .WithMessage("El cliente es obligatorio.");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("El email es obligatorio.")
                .MaximumLength(100)
                .WithMessage("El email no puede superar 100 caracteres.")
                .EmailAddress()
                .WithMessage("El formato del email no es válido.");
        }
    }
}
