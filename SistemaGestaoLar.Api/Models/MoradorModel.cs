using SistemaGestaoLar.Api.Entities;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Models
{
    public class MoradorModel
    {
        public MoradorModel() { }

        public MoradorModel(Morador entidade)
        {
            Id = entidade.Id;
            NomeCompleto = entidade.NomeCompleto;
            DataNascimento = entidade.DataNascimento;
            ContatoEmergencia = entidade.ContatoEmergencia;
            Observacoes = entidade.Observacoes;
        }

        public int Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string ContatoEmergencia { get; set; }

        public string Observacoes { get; set; }
    }
}
