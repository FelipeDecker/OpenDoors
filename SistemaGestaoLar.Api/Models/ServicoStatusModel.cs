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
            NomeServico = entidade.NomeServico;
            Status = entidade.Status;
            Justificativa = entidade.Justificativa;
        }

        [Required]
        public string NomeServico { get; set; }

        public ServicoStatusEnum Status { get; set; }

        public string Justificativa { get; set; }
    }
}
