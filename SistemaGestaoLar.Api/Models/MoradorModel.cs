using System;
using System.ComponentModel.DataAnnotations;
using SistemaGestaoLar.Api.Entities;

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
            HistoricoAcolhimento = entidade.HistoricoAcolhimento;
        }

        public int Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        public DateTime DataNascimento { get; set; }

        [Required]
        public string ContatoEmergencia { get; set; }

        [Required]
        public string Observacoes { get; set; }

        [Required]
        public string HistoricoAcolhimento { get; set; }
    }
}
