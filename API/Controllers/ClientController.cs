using IzumuClientes.Application.DTOs;
using IzumuClientes.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IzumuClientes.API.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ClienteController : ControllerBase
    {
        private readonly IClientService _service;

        public ClienteController(IClientService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerClientes()
        {
            var result = await _service.GetClients();

            if (!result.IsSuccess)
                return NotFound(new { success = false, message = result.ErrorMessage });

            return Ok(new { success = true, data = result.Data });
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ObtenerClientePorId(int id)
        {
            var result = await _service.GetbyIDClient(id);

            if (!result.IsSuccess)
                return NotFound(new { success = false, message = result.ErrorMessage });

            return Ok(new { success = true, data = result.Data });
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CrearCliente([FromBody] ClientRequestDTO dto)
        {
            var result = await _service.CreateClient(dto);

            if (!result.IsSuccess)
                return BadRequest(new { success = false, message = result.ErrorMessage });

            return CreatedAtAction(
                nameof(ObtenerClientePorId),
                new { id = result.Data },
                new { success = true, idCliente = result.Data, message = "Cliente creado exitosamente." }
            );
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> ActualizarCliente(int id, [FromBody] ClientRequestDTO dto)
        {
            var result = await _service.ModifyClient(id, dto);

            if (!result.IsSuccess)
                return BadRequest(new { success = false, message = result.ErrorMessage });

            return Ok(new { success = true, message = "Cliente actualizado exitosamente." });
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EliminarCliente(int id)
        {
            var result = await _service.RemoveClient(id);

            if (!result.IsSuccess)
                return NotFound(new { success = false, message = result.ErrorMessage });

            return Ok(new { success = true, message = "Cliente eliminado exitosamente." });
        }
    }
}
