using FluentValidation;
using IzumuClientes.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.Validators
{
    public class AddressValidator : AbstractValidator<AddressRequestDTO>
    {
        public AddressValidator()
        {
            RuleFor(x => x.IDCliente)
                .GreaterThan(0)
                .WithMessage("El cliente es obligatorio.");

            RuleFor(x => x.Direccion)
                .NotEmpty()
                .WithMessage("La dirección es obligatoria.")
                .MaximumLength(100)
                .WithMessage("La dirección no puede superar 100 caracteres.");
        }
    }
}
