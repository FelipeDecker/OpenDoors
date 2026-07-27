using System.ComponentModel.DataAnnotations;

namespace SistemaGestaoLar.Api.Entities
{
    public class ServicoTicket
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}
