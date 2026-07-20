using Microsoft.AspNetCore.Mvc;
using SistemaGestaoLar.Api.Models;
using SistemaGestaoLar.Api.Services;

namespace SistemaGestaoLar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ServicosStatusController : ControllerBase
    {
        private readonly ServicoStatusService _service;

        public ServicosStatusController(ServicoStatusService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ServicoStatusModel>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> Get()
        {
            var items = await _service.GetAllAsync();
            var models = new List<ServicoStatusModel>();
            foreach (var m in items) models.Add(new ServicoStatusModel(m));
            return Ok(models);
        }
    }
}
