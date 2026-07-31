using System;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Entities
{
    public class Morador
    {
        public int Id { get; set; }

        [Required]
        public string NomeCompleto { get; set; }

        public DateTime? DataNascimento { get; set; }

        public string ContatoEmergencia { get; set; }

        public string Observacoes { get; set; }
    }
}
