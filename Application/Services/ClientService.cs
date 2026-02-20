using FluentValidation;
using IzumuClientes.Application.DTOs;
using IzumuClientes.Application.Interfaces;
using IzumuClientes.Domain.Common;
using IzumuClientes.Domain.Interfaces;
using IzumuClientes.Domain.Entities;

namespace IzumuClientes.API.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly IValidator<ClientRequestDTO> _validator;

        public ClientService(IClientRepository repository, IValidator<ClientRequestDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<IEnumerable<ClientResponseDTO>>> GetClients()
        {
            var result = await _repository.SelectClients();
            if (!result.IsSuccess)
                return Result<IEnumerable<ClientResponseDTO>>.Failure(result.ErrorMessage!);

            var clients = result.Data!.Select(MapToDto);
            return Result<IEnumerable<ClientResponseDTO>>.Success(clients);
        }

        public async Task<Result<ClientResponseDTO>> GetbyIDClient(int idClient)
        {
            var result = await _repository.SelectbyIDClient(idClient);
            if (!result.IsSuccess)
                return Result<ClientResponseDTO>.Failure(result.ErrorMessage!);

            return Result<ClientResponseDTO>.Success(MapToDto(result.Data!));
        }

        public async Task<Result<int>> CreateClient(ClientRequestDTO client)
        {
            var validation = await _validator.ValidateAsync(client);
            if (!validation.IsValid)
            {
                var errors = string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage));
                return Result<int>.Failure(errors);
            }

            var cliente = MapToEntity(client);
            return await _repository.InsertClient(cliente);
        }

        public async Task<Result<bool>> ModifyClient(int idClient, ClientRequestDTO client)
        {
            var exists = await _repository.SelectbyIDClient(idClient);
            if (!exists.IsSuccess)
                return Result<bool>.Failure("El cliente no existe o fue eliminado.");

            var validation = await _validator.ValidateAsync(client);
            if (!validation.IsValid)
            {
                var errors = string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage));
                return Result<bool>.Failure(errors);
            }

            var cliente = MapToEntity(client);
            cliente.IDCliente = idClient;
            return await _repository.UpdateClient(cliente);
        }

        public async Task<Result<bool>> RemoveClient(int idClient)
        {
            var exists = await _repository.SelectbyIDClient(idClient);
            if (!exists.IsSuccess)
                return Result<bool>.Failure("El cliente no existe o fue eliminado.");

            return await _repository.DeleteClient(idClient);
        }

        private static ClientResponseDTO MapToDto(ClientEntity client) => new()
        {
            IDCliente = client.IDCliente,
            TipoIdentificacion = client.TipoIdentificacion,
            Identificacion = client.Identificacion,
            PrimerNombre = client.PrimerNombre,
            SegundoNombre = client.SegundoNombre,
            PrimerApellido = client.PrimerApellido,
            SegundoApellido = client.SegundoApellido,
            NombreCliente = client.NombreCliente,
            FechaNacimiento = client.FechaNacimiento,
            IDPlan = client.IDPlan,
            Telefonos = client.Telefonos,   
            Emails = client.Emails,     
            Direcciones = client.Direcciones 
        };

        private static ClientEntity MapToEntity(ClientRequestDTO client) => new()
        {
            TipoIdentificacion = client.TipoIdentificacion,
            Identificacion = client.Identificacion,
            PrimerNombre = client.PrimerNombre,
            SegundoNombre = client.SegundoNombre,
            PrimerApellido = client.PrimerApellido,
            SegundoApellido = client.SegundoApellido,
            FechaNacimiento = DateTime.ParseExact(
                            client.FechaNacimiento,
                            "dd-MM-yyyy",
                            System.Globalization.CultureInfo.InvariantCulture)
                            .ToString("yyyy-MM-dd"),
            IDPlan = client.IDPlan,
            Telefonos = client.Telefonos != null && client.Telefonos.Count > 0
                        ? System.Text.Json.JsonSerializer.Serialize(
                            client.Telefonos.Select(t => new { telefono = t.Telefono, movil = t.Movil }))
                        : null,
            Emails = client.Emails != null && client.Emails.Count > 0
                        ? System.Text.Json.JsonSerializer.Serialize(
                            client.Emails.Select(e => new { email = e.Email }))
                        : null,
            Direcciones = client.Direcciones != null && client.Direcciones.Count > 0
                        ? System.Text.Json.JsonSerializer.Serialize(
                            client.Direcciones.Select(d => new { direccion = d.Direccion }))
                        : null
        };
    }
}
