using System.Text;
using System.Text.Json;
using IzumuClientes.Web.Models;
using IzumuClientes.Web.Services;

namespace Web.Services
{
    public class ClienteApiService : IClientApiService
    {
        private readonly HttpClient _httpClient;
        private readonly JsonSerializerOptions _jsonOptions;

        public ClienteApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        }

        public async Task<List<ClientViewModel>> ObtenerClientes()
        {
            var response = await _httpClient.GetAsync("api/cliente");
            if (!response.IsSuccessStatusCode) return new();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<ClientViewModel>>>(json, _jsonOptions);
            return result?.Data ?? new();
        }

        public async Task<ClienteFormViewModel?> ObtenerClientePorId(int id)
        {
            var response = await _httpClient.GetAsync($"api/cliente/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<ClienteFormViewModel>>(json, _jsonOptions);
            return result?.Data;
        }

        public async Task<(bool success, string message)> CrearCliente(ClienteFormViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("api/cliente", content);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<object>>(json, _jsonOptions);
            return (response.IsSuccessStatusCode, result?.Message ?? "Error al crear el cliente.");
        }

        public async Task<(bool success, string message)> ActualizarCliente(int id, ClienteFormViewModel model)
        {
            var content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/cliente/{id}", content);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<object>>(json, _jsonOptions);
            return (response.IsSuccessStatusCode, result?.Message ?? "Error al actualizar el cliente.");
        }

        public async Task<(bool success, string message)> EliminarCliente(int id)
        {
            var response = await _httpClient.DeleteAsync($"api/cliente/{id}");
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<object>>(json, _jsonOptions);
            return (response.IsSuccessStatusCode, result?.Message ?? "Error al eliminar el cliente.");
        }

        public async Task<List<TipoDocumentoViewModel>> ObtenerTiposDocumento()
        {
            var response = await _httpClient.GetAsync("api/tipodocumento");
            if (!response.IsSuccessStatusCode) return new();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<TipoDocumentoViewModel>>>(json, _jsonOptions);
            return result?.Data ?? new();
        }

        public async Task<List<PlanViewModel>> ObtenerPlanes()
        {
            var response = await _httpClient.GetAsync("api/plan");
            if (!response.IsSuccessStatusCode) return new();

            var json = await response.Content.ReadAsStringAsync();
            var result = JsonSerializer.Deserialize<ApiResponse<List<PlanViewModel>>>(json, _jsonOptions);
            return result?.Data ?? new();
        }
    }
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
    }
}
