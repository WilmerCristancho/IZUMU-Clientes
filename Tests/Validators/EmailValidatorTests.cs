using FluentAssertions;
using FluentValidation.Validators;
using IzumuClientes.Application.DTOs;
using IzumuClientes.Application.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Validators
{
    public class EmailValidatorTests
    {
        private readonly EmailValidator _validator;

        public EmailValidatorTests()
        {
            _validator = new EmailValidator();
        }

        [Fact]
        public async Task Validar_CuandoEmailValido_DebeSerValido()
        {
            var email = new EmailRequestDTO
            {
                IDCliente = 1,
                Email = "carlos@example.com"
            };

            var result = await _validator.ValidateAsync(email);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validar_CuandoEmailSinArroba_DebeSerInvalido()
        {
            var email = new EmailRequestDTO
            {
                IDCliente = 1,
                Email = "correosinvalido.com"
            };

            var result = await _validator.ValidateAsync(email);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "El formato del email no es válido.");
        }

        [Fact]
        public async Task Validar_CuandoEmailVacio_DebeSerInvalido()
        {
            var email = new EmailRequestDTO
            {
                IDCliente = 1,
                Email = ""
            };

            var result = await _validator.ValidateAsync(email);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "El email es obligatorio.");
        }

    }
}
