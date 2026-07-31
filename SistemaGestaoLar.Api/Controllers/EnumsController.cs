using Microsoft.AspNetCore.Mvc;
using SistemaGestaoLar.Api.Enums;
using SistemaGestaoLar.Api.Helpers;
using SistemaGestaoLar.Api.Models;

namespace SistemaGestaoLar.Api.Controllers
{
    [ApiController]
    [Route("api/enums")]
    public class EnumsController : ControllerBase
    {
        [HttpGet("servicos-ticket")]
        [ProducesResponseType(typeof(IEnumerable<EnumItemModel>), 200)]
        public IActionResult GetServicosTicket()
        {
            return Ok(EnumDisplayHelper.GetItems<ServicoTicketEnum>());
        }

        [HttpGet("servicos-status")]
        [ProducesResponseType(typeof(IEnumerable<EnumItemModel>), 200)]
        public IActionResult GetServicosStatus()
        {
            return Ok(EnumDisplayHelper.GetItems<ServicoStatusEnum>());
        }
    }
}
