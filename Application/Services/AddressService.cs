using FluentValidation;
using IzumuClientes.Application.DTOs;
using IzumuClientes.Application.Interfaces;
using IzumuClientes.Domain.Common;
using IzumuClientes.Domain.Entities;
using IzumuClientes.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.Services
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _repository;
        private readonly IValidator<AddressRequestDTO> _validator;

        public AddressService(IAddressRepository repository, IValidator<AddressRequestDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<int>> CreateAddress(AddressRequestDTO address)
        {
            var validation = await _validator.ValidateAsync(address);
            if (!validation.IsValid)
            {
                var errores = string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage));
                return Result<int>.Failure(errores);
            }

            var direccion = new AddressEntity
            {
                IDCliente = address.IDCliente,
                DireccionResidencia = address.Direccion
            };

            return await _repository.InsertAddress(direccion);
        }
        
    }
}
