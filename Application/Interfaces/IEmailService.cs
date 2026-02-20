using IzumuClientes.Application.DTOs;
using IzumuClientes.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.Interfaces
{
    public interface IEmailService
    {
        Task<Result<int>> CreateEmail(EmailRequestDTO email);
    }
}
