namespace SistemaGestaoLar.Api.Models
{
    public class EnumItemModel
    {
        public EnumItemModel() { }

        public EnumItemModel(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        public int Id { get; set; }

        public string Nome { get; set; }
    }
}
