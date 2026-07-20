using SistemaGestaoLar.Api.Enums;

namespace SistemaGestaoLar.Api.Entities
{
    public class TicketServico
    {
        public int Id { get; set; }

        public int TicketDiarioId { get; set; }

        public TicketDiario TicketDiario { get; set; }

        public int ServicoStatusId { get; set; }

        public ServicoStatus ServicoStatus { get; set; }

        public ServicoStatusEnum Status { get; set; }

        public string Justificativa { get; set; }
    }
}
