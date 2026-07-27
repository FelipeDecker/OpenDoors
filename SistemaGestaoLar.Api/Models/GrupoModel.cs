using System.ComponentModel.DataAnnotations;
using SistemaGestaoLar.Api.Entities;

namespace SistemaGestaoLar.Api.Models
{
    public class GrupoModel
    {
        public GrupoModel() { }

        public GrupoModel(Grupo entidade)
        {
            Id = entidade.Id;
            Nome = entidade.Nome;
            Descricao = entidade.Descricao;
        }

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }
    }
}
