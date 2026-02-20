using IzumuClientes.Application.DTOs;
using IzumuClientes.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IzumuClientes.Application.Interfaces
{
    public interface IAddressService
    {
        Task<Result<int>> CreateAddress(AddressRequestDTO address);

    }
}
