using IzumuClientes.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IzumuClientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TipoDocumentoController : ControllerBase
    {
        private readonly ITypeDocumentRepository _repository;

        public TipoDocumentoController(ITypeDocumentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerTiposDocumento()
        {
            var result = await _repository.SelectTypeDocument();
            if (!result.IsSuccess)
                return NotFound(new { success = false, message = result.ErrorMessage });

            return Ok(new { success = true, data = result.Data });
        }
    }
}
