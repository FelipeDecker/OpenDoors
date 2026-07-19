using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using SistemaGestaoLar.Api.Entities;

namespace SistemaGestaoLar.Api.Models
{
    public class AjudanteModel
    {
        public AjudanteModel() { }

        public AjudanteModel(Ajudante entidade)
        {
            Id = entidade.Id;
            Nome = entidade.Nome;
            Email = entidade.Email;
            Telefone = entidade.Telefone;
            Disponibilidade = entidade.Disponibilidade;
        }

        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Email { get; set; }

        [Required]
        public string Telefone { get; set; }

        public string Disponibilidade { get; set; }
    }
}
