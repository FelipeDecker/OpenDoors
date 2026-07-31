using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Enums
{
    public enum ServicoTicketEnum
    {
        [Display(Name = "Jantar")]
        Jantar = 1,

        [Display(Name = "Banho")]
        Banho = 2,

        [Display(Name = "Troca de Roupas")]
        TrocaRoupas = 3,
    }
}
