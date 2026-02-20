using IzumuClientes.Application.DTOs;
using IzumuClientes.Domain.Common;

namespace IzumuClientes.Application.Interfaces
{
    public interface IClientService
    {
        Task<Result<IEnumerable<ClientResponseDTO>>> GetClients();
        Task<Result<ClientResponseDTO>> GetbyIDClient(int idClient);
        Task<Result<int>> CreateClient(ClientRequestDTO client);
        Task<Result<bool>> ModifyClient(int idClient, ClientRequestDTO client);
        Task<Result<bool>> RemoveClient(int idClient);
    }
}
