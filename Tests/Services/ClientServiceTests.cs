using FluentAssertions;
using FluentValidation;
using FluentValidation.Results;
using IzumuClientes.API.Services;
using IzumuClientes.Application.DTOs;
using IzumuClientes.Domain.Common;
using IzumuClientes.Domain.Entities;
using IzumuClientes.Domain.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class ClientServiceTests
    {
        private readonly Mock<IClientRepository> _repositoryMock;
        private readonly Mock<IValidator<ClientRequestDTO>> _validatorMock;
        private readonly ClientService _service;

        public ClientServiceTests()
        {
            _repositoryMock = new Mock<IClientRepository>();
            _validatorMock = new Mock<IValidator<ClientRequestDTO>>();
            _service = new ClientService(_repositoryMock.Object, _validatorMock.Object);
        }

        [Fact]
        public async Task ObtenerClientes_CuandoExistenClientes_DebeRetornarListaExitosa()
        {
            var clients = new List<ClientEntity>
            {
                new ClientEntity
                {
                    IDCliente          = 1,
                    TipoIdentificacion = 1,
                    Identificacion     = "123456789",
                    PrimerNombre       = "Carlos",
                    PrimerApellido     = "Rodríguez",
                    FechaNacimiento    = "15-06-1990",
                    IDPlan             = 1
                }
            };

            _repositoryMock
                .Setup(r => r.SelectClients())
                .ReturnsAsync(Result<IEnumerable<ClientEntity>>.Success(clients));

            var result = await _service.GetClients();

            result.IsSuccess.Should().BeTrue();
            result.Data.Should().HaveCount(1);
            result.Data!.First().PrimerNombre.Should().Be("Carlos");
        }

        [Fact]
        public async Task ObtenerClientes_CuandoRepositorioFalla_DebeRetornarFallido()
        {
            _repositoryMock
                .Setup(r => r.SelectClients())
                .ReturnsAsync(Result<IEnumerable<ClientEntity>>.Failure("Error de conexión"));

            var result = await _service.GetClients();

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Error de conexión");
        }

        [Fact]
        public async Task ObtenerClientPorId_CuandoClienteExiste_DebeRetornarCliente()
        {
            var client = new ClientEntity
            {
                IDCliente = 1,
                TipoIdentificacion = 1,
                Identificacion = "123456789",
                PrimerNombre = "Carlos",
                PrimerApellido = "Rodríguez",
                FechaNacimiento = "15-06-1990",
                IDPlan = 1
            };

            _repositoryMock
                .Setup(r => r.SelectbyIDClient(1))
                .ReturnsAsync(Result<ClientEntity>.Success(client));

            var result = await _service.GetbyIDClient(1);

            result.IsSuccess.Should().BeTrue();
            result.Data!.IDCliente.Should().Be(1);
            result.Data.Identificacion.Should().Be("123456789");
        }

        [Fact]
        public async Task ObtenerClientePor_CuandoClienteNoExiste_DebeRetornarFallido()
        {
            _repositoryMock
                .Setup(r => r.SelectbyIDClient(99))
                .ReturnsAsync(Result<ClientEntity>.Failure("Cliente no encontrado."));

            var result = await _service.GetbyIDClient(99);

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("Cliente no encontrado.");
        }

        [Fact]
        public async Task CrearCliente_CuandoDatosValidos_DebeCrearClienteExitosamente()
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

            _validatorMock
                .Setup(v => v.ValidateAsync(client, default))
                .ReturnsAsync(new ValidationResult());

            _repositoryMock
                .Setup(r => r.InsertClient(It.IsAny<ClientEntity>()))
                .ReturnsAsync(Result<int>.Success(1));

            var result = await _service.CreateClient(client);

            result.IsSuccess.Should().BeTrue();
            result.Data.Should().Be(1);
        }

        [Fact]
        public async Task CrearCliente_CuandoValidacionFalla_DebeRetornarErrores()
        {
            var client = new ClientRequestDTO
            {
                TipoIdentificacion = 0, 
                Identificacion = "", 
                PrimerNombre = "",
                PrimerApellido = "",
                FechaNacimiento = "",
                IDPlan = 0
            };

            var validationFailures = new List<ValidationFailure>
            {
                new ValidationFailure("TipoIdentificacion", "El tipo de identificación es obligatorio."),
                new ValidationFailure("Identificacion", "La identificación es obligatoria.")
            };

            _validatorMock
                .Setup(v => v.ValidateAsync(client, default))
                .ReturnsAsync(new ValidationResult(validationFailures));

            var result = await _service.CreateClient(client);

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Contain("El tipo de identificación es obligatorio.");
        }

        [Fact]
        public async Task ActualizarCliente_CuandoClienteExisteYDatosValidos_DebeActualizarExitosamente()
        {
            var client = new ClientRequestDTO
            {
                TipoIdentificacion = 1,
                Identificacion = "123456789",
                PrimerNombre = "Carlos",
                PrimerApellido = "Rodríguez",
                FechaNacimiento = "15-06-1990",
                IDPlan = 2
            };

            var existingClient = new ClientEntity
            {
                IDCliente = 1,
                TipoIdentificacion = 1,
                Identificacion = "123456789",
                PrimerNombre = "Carlos",
                PrimerApellido = "Rodríguez",
                FechaNacimiento = "15-06-1990",
                IDPlan = 1
            };

            _repositoryMock
                .Setup(r => r.SelectbyIDClient(1))
                .ReturnsAsync(Result<ClientEntity>.Success(existingClient));

            _validatorMock
                .Setup(v => v.ValidateAsync(client, default))
                .ReturnsAsync(new ValidationResult());

            _repositoryMock
                .Setup(r => r.UpdateClient(It.IsAny<ClientEntity>()))
                .ReturnsAsync(Result<bool>.Success(true));

            var result = await _service.ModifyClient(1, client);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task ActualizarCliente_CuandoClienteNoExiste_DebeRetornarFallido()
        {
            _repositoryMock
                .Setup(r => r.SelectbyIDClient(99))
                .ReturnsAsync(Result<ClientEntity>.Failure("Cliente no encontrado."));

            var result = await _service.ModifyClient(99, new ClientRequestDTO());

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("El cliente no existe o fue eliminado.");
        }

        [Fact]
        public async Task EliminarCliente_CuandoClienteExiste_DebeEliminarExitosamente()
        {
            var cliente = new ClientEntity { IDCliente = 1, Identificacion = "123456789" };

            _repositoryMock
                .Setup(r => r.SelectbyIDClient(1))
                .ReturnsAsync(Result<ClientEntity>.Success(cliente));

            _repositoryMock
                .Setup(r => r.DeleteClient(1))
                .ReturnsAsync(Result<bool>.Success(true));

            var result = await _service.RemoveClient(1);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public async Task EliminarAsync_CuandoClienteNoExiste_DebeRetornarFallido()
        {
            _repositoryMock
                .Setup(r => r.SelectbyIDClient(99))
                .ReturnsAsync(Result<ClientEntity>.Failure("Cliente no encontrado."));

            var result = await _service.RemoveClient(99);

            result.IsSuccess.Should().BeFalse();
            result.ErrorMessage.Should().Be("El cliente no existe o fue eliminado.");
        }
    }
}
