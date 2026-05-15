using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Entities
{
    public class Morador
    {
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
