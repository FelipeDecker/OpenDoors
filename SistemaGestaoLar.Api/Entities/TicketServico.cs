using SistemaGestaoLar.Api.Enums;

namespace SistemaGestaoLar.Api.Entities
{
    public class TicketServico
    {
        public int Id { get; set; }
        public int TicketDiarioId { get; set; }
        public int ServicoStatusId { get; set; }
        public int ServicoTicketId { get; set; }

        public TicketDiario TicketDiario { get; set; }
        public ServicoStatus ServicoStatus { get; set; }
        public ServicoTicket ServicoTicket { get; set; }
    }
}
