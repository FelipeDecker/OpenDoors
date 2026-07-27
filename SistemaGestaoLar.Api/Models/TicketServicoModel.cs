using System;
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
            ServicoTicket = Enum.TryParse<ServicoTicketEnum>(entidade.ServicoTicket?.Name, out var servico)
                ? servico
                : default;
            Status = Enum.TryParse<ServicoStatusEnum>(entidade.ServicoStatus?.Name, out var status)
                ? status
                : default;
        }

        public int Id { get; set; }

        [Required]
        public ServicoTicketEnum ServicoTicket { get; set; }

        [Required]
        public ServicoStatusEnum Status { get; set; }
    }
}
