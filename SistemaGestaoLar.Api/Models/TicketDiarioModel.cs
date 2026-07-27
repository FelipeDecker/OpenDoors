using SistemaGestaoLar.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Models
{
    public class TicketDiarioModel
    {
        public TicketDiarioModel() { }

        public TicketDiarioModel(TicketDiario entidade)
        {
            Id = entidade.Id;
            MoradorId = entidade.MoradorId;
            DataServico = entidade.DataServico;
            if (entidade.Servicos != null)
            {
                Servicos = new List<TicketServicoModel>();
                foreach (var s in entidade.Servicos)
                {
                    Servicos.Add(new TicketServicoModel(s));
                }
            }
        }

        public int Id { get; set; }

        [Required]
        public int MoradorId { get; set; }

        [Required]
        public DateOnly DataServico { get; set; }

        [Required]
        public List<TicketServicoModel> Servicos { get; set; }
    }
}
