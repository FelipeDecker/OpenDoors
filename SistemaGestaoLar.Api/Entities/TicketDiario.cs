using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Entities
{
    public class TicketDiario
    {
        public int Id { get; set; }

        public int MoradorId { get; set; }

        public DateOnly DataServico { get; set; }

        [Required]
        public List<ServicoStatus> Servicos { get; set; }
    }
}
