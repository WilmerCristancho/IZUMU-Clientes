using IzumuClientes.Application.DTOs;
using IzumuClientes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IzumuClientes.API.Controllers
{
    [ApiController]
    [Route("api/cliente/{idCliente:int}/email")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _service;
        public EmailController(IEmailService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> CrearEmail(int idCliente, [FromBody] EmailRequestDTO dto)
        {
            dto.IDCliente = idCliente;
            var result = await _service.CreateEmail(dto);
            if (!result.IsSuccess)
                return BadRequest(new { success = false, message = result.ErrorMessage });
            return StatusCode(201, new { success = true, idEmail = result.Data, message = "Email registrado exitosamente." });
        }
    }
}
