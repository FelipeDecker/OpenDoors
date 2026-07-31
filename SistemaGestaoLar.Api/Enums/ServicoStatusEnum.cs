using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Enums
{
    public enum ServicoStatusEnum
    {
        [Display(Name = "Pendente")]
        Pendente = 1,

        [Display(Name = "Realizado")]
        Realizado = 2,

        [Display(Name = "Não Realizado")]
        NaoRealizado = 3
    }
}
