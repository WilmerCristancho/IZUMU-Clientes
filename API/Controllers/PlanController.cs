using IzumuClientes.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace IzumuClientes.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlanController : ControllerBase
    {
        private readonly IPlanRepository _repository;

        public PlanController(IPlanRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> ObtenerPlanes()
        {
            var result = await _repository.SelectPlans();
            if (!result.IsSuccess)
                return NotFound(new { success = false, message = result.ErrorMessage });

            return Ok(new { success = true, data = result.Data });
        }
    }
}
