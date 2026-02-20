using IzumuClientes.Application.DTOs;
using IzumuClientes.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.Interfaces
{
    public interface IPhoneService
    {
        Task<Result<int>> CreatePhone(PhoneRequestDTO phone);

    }
}
