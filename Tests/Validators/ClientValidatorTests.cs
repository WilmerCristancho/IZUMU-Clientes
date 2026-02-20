using FluentAssertions;
using IzumuClientes.API.Validators;
using IzumuClientes.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Validators
{
    public class ClientValidatorTests
    {
        private readonly ClientValidator _validator;

        public ClientValidatorTests()
        {
            _validator = new ClientValidator();
        }

        [Fact]
        public async Task Validar_CuandoDatosCompletos_DebeSerValido()
        {
            var client = new ClientRequestDTO
            {
                TipoIdentificacion = 1,
                Identificacion = "123456789",
                PrimerNombre = "Carlos",
                PrimerApellido = "Rodríguez",
                FechaNacimiento = "15-06-1990",
                IDPlan = 1
            };

            var result = await _validator.ValidateAsync(client);

            result.IsValid.Should().BeTrue();
        }

        [Fact]
        public async Task Validar_CuandoIdentificacionTieneLetras_DebeSerInvalido()
        {
            var client = new ClientRequestDTO
            {
                TipoIdentificacion = 1,
                Identificacion = "ABC123",
                PrimerNombre = "Carlos",
                PrimerApellido = "Rodríguez",
                FechaNacimiento = "15-06-1990",
                IDPlan = 1
            };

            var result = await _validator.ValidateAsync(client);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "La identificación solo debe contener números.");
        }

        [Fact]
        public async Task Validar_CuandoFechaFormatoIncorrecto_DebeSerInvalido()
        {
            var client = new ClientRequestDTO
            {
                TipoIdentificacion = 1,
                Identificacion = "123456789",
                PrimerNombre = "Carlos",
                PrimerApellido = "Rodríguez",
                FechaNacimiento = "1990-06-15", 
                IDPlan = 1
            };

            var result = await _validator.ValidateAsync(client);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "El formato de la fecha debe ser dd-MM-yyyy.");
        }

        [Fact]
        public async Task Validar_CuandoClienteEsMenorDeEdad_DebeSerInvalido()
        {
            var client = new ClientRequestDTO
            {
                TipoIdentificacion = 1,
                Identificacion = "123456789",
                PrimerNombre = "Carlos",
                PrimerApellido = "Rodríguez",
                FechaNacimiento = "15-06-2015", 
                IDPlan = 1
            };

            var result = await _validator.ValidateAsync(client);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "El cliente debe ser mayor de edad.");
        }

        [Fact]
        public async Task Validar_CuandoNombreTieneNumeros_DebeSerInvalido()
        {
            var client = new ClientRequestDTO
            {
                TipoIdentificacion = 1,
                Identificacion = "123456789",
                PrimerNombre = "Carlos123", 
                PrimerApellido = "Rodríguez",
                FechaNacimiento = "15-06-1990",
                IDPlan = 1
            };

            var result = await _validator.ValidateAsync(client);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().Contain(e => e.ErrorMessage == "El primer nombre solo debe contener letras.");
        }
    }
}
