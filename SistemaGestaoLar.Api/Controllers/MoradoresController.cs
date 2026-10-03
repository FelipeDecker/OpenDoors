using Microsoft.AspNetCore.Mvc;
using SistemaGestaoLar.Api.Entities;
using SistemaGestaoLar.Api.Models;
using SistemaGestaoLar.Api.Services;

namespace SistemaGestaoLar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoradoresController : ControllerBase
    {
        private readonly MoradorService _service;
        private readonly RelatorioService _relatorioService;

        public MoradoresController(MoradorService service, RelatorioService relatorioService)
        {
            _service = service;
            _relatorioService = relatorioService;
        }

        [HttpGet("relatorio-mensal")]
        [ProducesResponseType(typeof(FileContentResult), 200, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> RelatorioMensal()
        {
            var (conteudo, nomeArquivo) = await _relatorioService.GerarRelatorioMesAnteriorAsync();
            return File(conteudo, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nomeArquivo);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<MoradorModel>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> Get()
        {
            var items = await _service.GetAllAsync();
            var models = new List<MoradorModel>();
            foreach (var m in items) models.Add(new MoradorModel(m));
            return Ok(models);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(MoradorModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return BadRequest(new ErrorResponseModel { Errors = "Morador não encontrado" });
            return Ok(new MoradorModel(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(MoradorModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Create([FromBody] MoradorModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ErrorResponseModel { Errors = "Modelo inválido" });
            var entidade = new Morador
            {
                NomeCompleto = model.NomeCompleto,
                DataNascimento = model.DataNascimento,
                ContatoEmergencia = model.ContatoEmergencia,
                Observacoes = model.Observacoes
            };
            var created = await _service.CreateAsync(entidade);
            return Ok(new MoradorModel(created));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(MoradorModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Update(int id, [FromBody] MoradorModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ErrorResponseModel { Errors = "Modelo inválido" });
            var entidade = await _service.GetByIdAsync(id);
            if (entidade == null) return BadRequest(new ErrorResponseModel { Errors = "Morador não encontrado" });
            entidade.NomeCompleto = model.NomeCompleto;
            entidade.DataNascimento = model.DataNascimento;
            entidade.ContatoEmergencia = model.ContatoEmergencia;
            entidade.Observacoes = model.Observacoes;
            var updated = await _service.UpdateAsync(entidade);
            return Ok(new MoradorModel(updated));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            if (await _service.PossuiTicketsAsync(id))
                return BadRequest(new ErrorResponseModel { Errors = "Não é possível excluir o morador pois ele possui tickets diários associados" });
            var ok = await _service.DeleteAsync(id);
            if (!ok) return BadRequest(new ErrorResponseModel { Errors = "Morador não encontrado" });
            return Ok();
        }
    }
}
