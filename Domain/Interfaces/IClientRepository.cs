using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IzumuClientes.Domain.Common;
using IzumuClientes.Domain.Entities;

namespace IzumuClientes.Domain.Interfaces
{
    public interface IClientRepository
    {
        Task<Result<IEnumerable<ClientEntity>>> SelectClients();
        Task<Result<ClientEntity>> SelectbyIDClient(int idCliente);
        Task<Result<int>> InsertClient(ClientEntity client);
        Task<Result<bool>> UpdateClient(ClientEntity client);
        Task<Result<bool>> DeleteClient(int idCliente);

    }
}