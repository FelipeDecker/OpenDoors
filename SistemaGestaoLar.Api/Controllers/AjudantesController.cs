using Microsoft.AspNetCore.Mvc;
using SistemaGestaoLar.Api.Models;
using SistemaGestaoLar.Api.Services;
using SistemaGestaoLar.Api.Entities;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SistemaGestaoLar.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AjudantesController : ControllerBase
    {
        private readonly AjudanteService _service;

        public AjudantesController(AjudanteService service)
        {
            _service = service;
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<AjudanteModel>), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 500)]
        public async Task<IActionResult> Get()
        {
            var items = await _service.GetAllAsync();
            var models = new List<AjudanteModel>();
            foreach (var m in items) models.Add(new AjudanteModel(m));
            return Ok(models);
        }

        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(AjudanteModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _service.GetByIdAsync(id);
            if (item == null) return BadRequest(new ErrorResponseModel { Errors = "Ajudante não encontrado" });
            return Ok(new AjudanteModel(item));
        }

        [HttpPost]
        [ProducesResponseType(typeof(AjudanteModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Create([FromBody] AjudanteModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ErrorResponseModel { Errors = "Modelo inválido" });
            var entidade = new Ajudante
            {
                Nome = model.Nome,
                Email = model.Email,
                Telefone = model.Telefone,
                Disponibilidade = model.Disponibilidade,
                Habilidades = model.Habilidades,
                GruposAtribuidos = model.GruposAtribuidos
            };
            var created = await _service.CreateAsync(entidade);
            return Ok(new AjudanteModel(created));
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(typeof(AjudanteModel), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Update(int id, [FromBody] AjudanteModel model)
        {
            if (!ModelState.IsValid) return BadRequest(new ErrorResponseModel { Errors = "Modelo inválido" });
            var entidade = await _service.GetByIdAsync(id);
            if (entidade == null) return BadRequest(new ErrorResponseModel { Errors = "Ajudante não encontrado" });
            entidade.Nome = model.Nome;
            entidade.Email = model.Email;
            entidade.Telefone = model.Telefone;
            entidade.Disponibilidade = model.Disponibilidade;
            entidade.Habilidades = model.Habilidades;
            entidade.GruposAtribuidos = model.GruposAtribuidos;
            var updated = await _service.UpdateAsync(entidade);
            return Ok(new AjudanteModel(updated));
        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(typeof(void), 200)]
        [ProducesResponseType(typeof(ErrorResponseModel), 400)]
        public async Task<IActionResult> Delete(int id)
        {
            var ok = await _service.DeleteAsync(id);
            if (!ok) return BadRequest(new ErrorResponseModel { Errors = "Ajudante não encontrado" });
            return Ok();
        }
    }
}
