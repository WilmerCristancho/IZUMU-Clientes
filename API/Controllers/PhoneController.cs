using IzumuClientes.Application.DTOs;
using IzumuClientes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IzumuClientes.API.Controllers
{
    [ApiController]
    [Route("api/cliente/{idCliente:int}/telefono")]
    public class PhoneController : ControllerBase
    {
        private readonly IPhoneService _service;
        public PhoneController(IPhoneService service) => _service = service;

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearTelefono(int idCliente, [FromBody] PhoneRequestDTO dto)
        {
            dto.IDCliente = idCliente;
            var result = await _service.CreatePhone(dto);
            if (!result.IsSuccess)
                return BadRequest(new { success = false, message = result.ErrorMessage });
            return StatusCode(201, new { success = true, idTelefono = result.Data, message = "Teléfono registrado exitosamente." });
        }
    }
}
