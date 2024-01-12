namespace introApiWeb.Models
{
    public class Produto
    {
        public long Id { get; set; }

        public string? Nome { get; set; }

        public string? Descricao { get; set; }

        public int IdCategoria { get; set; }
    }
}
