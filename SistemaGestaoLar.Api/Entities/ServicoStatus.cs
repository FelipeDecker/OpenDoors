using System.ComponentModel.DataAnnotations;
using SistemaGestaoLar.Api.Enums;

namespace SistemaGestaoLar.Api.Entities
{
    public class ServicoStatus
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NomeServico { get; set; }

        public ServicoStatusEnum Status { get; set; }

        public string Justificativa { get; set; }
    }
}
