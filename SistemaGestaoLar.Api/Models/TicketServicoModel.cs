using System.ComponentModel.DataAnnotations;
using SistemaGestaoLar.Api.Entities;
using SistemaGestaoLar.Api.Enums;

namespace SistemaGestaoLar.Api.Models
{
    public class TicketServicoModel
    {
        public TicketServicoModel() { }

        public TicketServicoModel(TicketServico entidade)
        {
            Id = entidade.Id;
            ServicoStatusId = entidade.ServicoStatusId;
            NomeServico = entidade.ServicoStatus?.Name;
            Status = entidade.Status;
            Justificativa = entidade.Justificativa;
        }

        public int Id { get; set; }

        [Required]
        public int ServicoStatusId { get; set; }

        public string NomeServico { get; set; }

        public ServicoStatusEnum Status { get; set; }

        public string Justificativa { get; set; }
    }
}
