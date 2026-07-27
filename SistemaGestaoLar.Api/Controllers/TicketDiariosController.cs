using Microsoft.AspNetCore.Mvc;
using SistemaGestaoLar.Api.Entities;
using SistemaGestaoLar.Api.Models;
using SistemaGestaoLar.Api.Services;

namespace SistemaGestaoLar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketDiariosController : ControllerBase
    {
        private readonly TicketDiarioService _service;

        public TicketDiariosController(TicketDiarioService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TicketDiarioModel>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> Get()
        {
            var items = await _service.GetAllAsync();
            var models = new List<TicketDiarioModel>();
            foreach (var m in items) models.Add(new TicketDiarioModel(m));
            return Ok(models);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(TicketDiarioModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return BadRequest(new ErrorResponseModel { Errors = "Ticket não encontrado" });
            var model = new TicketDiarioModel(item);
            return Ok(model);
        }

        [HttpPost]
        [ProducesResponseType(typeof(TicketDiarioModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Create([FromBody] TicketDiarioModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ErrorResponseModel { Errors = "Modelo inválido" });
            var entidade = new TicketDiario
            {
                MoradorId = model.MoradorId,
                DataServico = model.DataServico,
                Servicos = new System.Collections.Generic.List<TicketServico>()
            };
            if (model.Servicos != null)
            {
                foreach (var s in model.Servicos)
                {
                    entidade.Servicos.Add(new TicketServico { ServicoTicketId = (int)s.ServicoTicket + 1, ServicoStatusId = (int)s.Status + 1 });
                }
            }
            var created = await _service.CreateAsync(entidade);
            return Ok(new TicketDiarioModel(created));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(TicketDiarioModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Update(int id, [FromBody] TicketDiarioModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ErrorResponseModel { Errors = "Modelo inválido" });
            var entidade = await _service.GetByIdAsync(id);
            if (entidade == null) return BadRequest(new ErrorResponseModel { Errors = "Ticket não encontrado" });
            entidade.MoradorId = model.MoradorId;
            entidade.DataServico = model.DataServico;
            entidade.Servicos = new System.Collections.Generic.List<TicketServico>();
            if (model.Servicos != null)
            {
                foreach (var s in model.Servicos)
                {
                    entidade.Servicos.Add(new TicketServico { ServicoTicketId = (int)s.ServicoTicket + 1, ServicoStatusId = (int)s.Status + 1 });
                }
            }
            var updated = await _service.UpdateAsync(entidade);
            return Ok(new TicketDiarioModel(updated));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return BadRequest(new ErrorResponseModel { Errors = "Ticket não encontrado" });
            return Ok();
        }
    }
}
