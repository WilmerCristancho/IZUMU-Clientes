using IzumuClientes.Web.Services;
using Microsoft.AspNetCore.Mvc;
using IzumuClientes.Web.Models;

namespace IzumuClientes.Web.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientApiService _service;

        public ClientController(IClientApiService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            var clientes = await _service.ObtenerClientes();
            return View(clientes);
        }

        public async Task<IActionResult> Form(int? id)
        {
            var model = new ClienteFormViewModel();

            if (id.HasValue)
            {
                var cliente = await _service.ObtenerClientePorId(id.Value);
                if (cliente != null) model = cliente;
            }

            model.TiposDocumento = await _service.ObtenerTiposDocumento();
            model.Planes = await _service.ObtenerPlanes();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Guardar(ClienteFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.TiposDocumento = await _service.ObtenerTiposDocumento();
                model.Planes = await _service.ObtenerPlanes();
                return View("Form", model);
            }

            (bool success, string message) result;

            if (model.IDCliente == 0)
                result = await _service.CrearCliente(model);
            else
                result = await _service.ActualizarCliente(model.IDCliente, model);

            if (!result.success)
            {
                ModelState.AddModelError(string.Empty, result.message);
                model.TiposDocumento = await _service.ObtenerTiposDocumento();
                model.Planes = await _service.ObtenerPlanes();
                return View("Form", model);
            }

            TempData["Success"] = result.message;
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Eliminar(int id)
        {
            var result = await _service.EliminarCliente(id);
            TempData[result.success ? "Success" : "Error"] = result.message;
            return RedirectToAction(nameof(Index));
        }
    }
}
