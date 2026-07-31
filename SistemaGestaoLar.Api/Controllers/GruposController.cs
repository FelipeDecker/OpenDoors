using Microsoft.AspNetCore.Mvc;
using SistemaGestaoLar.Api.Entities;
using SistemaGestaoLar.Api.Models;
using SistemaGestaoLar.Api.Services;

namespace SistemaGestaoLar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GruposController : ControllerBase
    {
        private readonly GrupoService _service;

        public GruposController(GrupoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GrupoModel>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> Get()
        {
            var items = await _service.GetAllAsync();
            var models = new List<GrupoModel>();
            foreach (var m in items) models.Add(new GrupoModel(m));
            return Ok(models);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(GrupoModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return BadRequest(new ErrorResponseModel { Errors = "Grupo não encontrado" });
            return Ok(new GrupoModel(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(GrupoModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Create([FromBody] GrupoModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ErrorResponseModel { Errors = "Modelo inválido" });
            var entidade = new Grupo
            {
                Nome = model.Nome,
                Descricao = model.Descricao
            };
            var created = await _service.CreateAsync(entidade);
            return Ok(new GrupoModel(created));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(GrupoModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Update(int id, [FromBody] GrupoModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ErrorResponseModel { Errors = "Modelo inválido" });
            var entidade = await _service.GetByIdAsync(id);
            if (entidade == null) return BadRequest(new ErrorResponseModel { Errors = "Grupo não encontrado" });
            entidade.Nome = model.Nome;
            entidade.Descricao = model.Descricao;
            var updated = await _service.UpdateAsync(entidade);
            return Ok(new GrupoModel(updated));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return BadRequest(new ErrorResponseModel { Errors = "Grupo não encontrado" });
            return Ok();
        }

        [HttpGet("{id:int}/ajudantes")]
        [ProducesResponseType(typeof(IEnumerable<AjudanteModel>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> GetAjudantes(int id)
        {
            var grupo = await _service.GetByIdAsync(id);
            if (grupo == null) return BadRequest(new ErrorResponseModel { Errors = "Grupo não encontrado" });

            var ajudantes = await _service.GetAjudantesDoGrupoAsync(id);
            var models = new List<AjudanteModel>();
            foreach (var a in ajudantes) models.Add(new AjudanteModel(a));
            return Ok(models);
        }

        [HttpGet("{id:int}/ajudantes-disponiveis")]
        [ProducesResponseType(typeof(IEnumerable<AjudanteModel>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> GetAjudantesDisponiveis(int id)
        {
            var grupo = await _service.GetByIdAsync(id);
            if (grupo == null) return BadRequest(new ErrorResponseModel { Errors = "Grupo não encontrado" });

            var ajudantes = await _service.GetAjudantesDisponiveisAsync(id);
            var models = new List<AjudanteModel>();
            foreach (var a in ajudantes) models.Add(new AjudanteModel(a));
            return Ok(models);
        }

        [HttpPost("{id:int}/ajudantes/{ajudanteId:int}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> AdicionarAjudante(int id, int ajudanteId)
        {
            var ok = await _service.AdicionarAjudanteAsync(id, ajudanteId);
            if (!ok) return BadRequest(new ErrorResponseModel { Errors = "Grupo ou ajudante não encontrado" });
            return Ok();
        }

        [HttpDelete("{id:int}/ajudantes/{ajudanteId:int}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> RemoverAjudante(int id, int ajudanteId)
        {
            var ok = await _service.RemoverAjudanteAsync(id, ajudanteId);
            if (!ok) return BadRequest(new ErrorResponseModel { Errors = "Grupo ou ajudante não vinculado" });
            return Ok();
        }
    }
}
