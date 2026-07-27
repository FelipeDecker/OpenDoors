using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Entities
{
    public class TicketDiario
    {
        public int Id { get; set; }

        [Required]
        public int MoradorId { get; set; }

        [Required]
        public DateOnly DataServico { get; set; }

        [Required]
        public List<TicketServico> Servicos { get; set; }
    }
}
