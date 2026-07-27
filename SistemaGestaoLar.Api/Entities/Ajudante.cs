using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Entities
{
    public class Ajudante
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        public string Disponibilidade { get; set; }

        public List<Grupo> Grupos { get; set; } = new();
    }
}
