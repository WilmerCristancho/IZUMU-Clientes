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
    public class PhoneService : IPhoneService
    {
        private readonly IPhoneRepository _repository;
        private readonly IValidator<PhoneRequestDTO> _validator;

        public PhoneService(IPhoneRepository repository, IValidator<PhoneRequestDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }
        public async Task<Result<int>> CreatePhone(PhoneRequestDTO dto)
        {
            var validation = await _validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                var errores = string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage));
                return Result<int>.Failure(errores);
            }

            var telefono = new PhoneEntity
            {
                IDCliente = dto.IDCliente,
                NumeroTelefono = dto.Telefono,
                Movil = dto.Movil
            };

            return await _repository.InsertPhone(telefono);
        }
    }
}
