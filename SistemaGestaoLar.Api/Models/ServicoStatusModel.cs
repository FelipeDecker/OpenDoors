using System.ComponentModel.DataAnnotations;
using SistemaGestaoLar.Api.Entities;

namespace SistemaGestaoLar.Api.Models
{
    public class ServicoStatusModel
    {
        public ServicoStatusModel() { }

        public ServicoStatusModel(ServicoStatus entidade)
        {
            Id = entidade.Id;
            Name = entidade.Name;
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
