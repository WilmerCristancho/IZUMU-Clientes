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
    public class EmailService : IEmailService
    {
        private readonly IEmailRepository _repository;
        private readonly IValidator<EmailRequestDTO> _validator;

        public EmailService(IEmailRepository repository, IValidator<EmailRequestDTO> validator)
        {
            _repository = repository;
            _validator = validator;
        }

        public async Task<Result<int>> CreateEmail(EmailRequestDTO email)
        {
            var validation = await _validator.ValidateAsync(email);
            if (!validation.IsValid)
            {
                var errores = string.Join(" | ", validation.Errors.Select(e => e.ErrorMessage));
                return Result<int>.Failure(errores);
            }

            var correo = new EmailEntity
            {
                IDCliente = email.IDCliente,
                CorreoEmail = email.Email
            };

            return await _repository.InsertEmail(correo);
        }        
    }
}
