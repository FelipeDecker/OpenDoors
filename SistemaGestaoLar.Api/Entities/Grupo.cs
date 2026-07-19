using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Entities
{
    public class Grupo
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        public string Descricao { get; set; }

        public List<Ajudante> Ajudantes { get; set; } = new();
    }
}
