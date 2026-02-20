using IzumuClientes.Web.Models;

namespace IzumuClientes.Web.Services
{
    public interface IClientApiService
    {
        Task<List<ClientViewModel>> ObtenerClientes();
        Task<ClienteFormViewModel?> ObtenerClientePorId(int id);
        Task<(bool success, string message)> CrearCliente(ClienteFormViewModel model);
        Task<(bool success, string message)> ActualizarCliente(int id, ClienteFormViewModel model);
        Task<(bool success, string message)> EliminarCliente(int id);
        Task<List<TipoDocumentoViewModel>> ObtenerTiposDocumento();
        Task<List<PlanViewModel>> ObtenerPlanes();
    }
}
