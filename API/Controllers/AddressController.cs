using IzumuClientes.Application.DTOs;
using IzumuClientes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IzumuClientes.API.Controllers
{
    [ApiController]
    [Route("api/cliente/{idCliente:int}/direccion")]
    public class DireccionController : ControllerBase
    {
        private readonly IAddressService _service;
        public DireccionController(IAddressService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> CrearDireccion(int idCliente, [FromBody] AddressRequestDTO dto)
        {
            dto.IDCliente = idCliente;
            var result = await _service.CreateAddress(dto);
            if (!result.IsSuccess)
                return BadRequest(new { success = false, message = result.ErrorMessage });
            return StatusCode(201, new { success = true, idDireccion = result.Data, message = "Dirección registrada exitosamente." });
        }     
    }
}
