using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Entities
{
    public class Ajudante
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        [Required]
        public string Disponibilidade { get; set; }

        [Required]
        public string Habilidades { get; set; }

        [Required]
        public List<string> GruposAtribuidos { get; set; }
    }
}
