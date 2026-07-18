using System.ComponentModel.DataAnnotations;
using SistemaGestaoLar.Api.Entities;
using SistemaGestaoLar.Api.Enums;

namespace SistemaGestaoLar.Api.Models
{
    public class ServicoStatusModel
    {
        public ServicoStatusModel() { }

        public ServicoStatusModel(ServicoStatus entidade)
        {
            Id = entidade.Id;
            NomeServico = entidade.NomeServico;
            Status = entidade.Status;
            Justificativa = entidade.Justificativa;
        }

        public int Id { get; set; }

        [Required]
        public string NomeServico { get; set; }

        public ServicoStatusEnum Status { get; set; }

        public string Justificativa { get; set; }
    }
}
