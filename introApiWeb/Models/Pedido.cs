using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace introApiWeb.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DataPedido { get; set; }

        public int PessoaId { get; set; }

        public Pessoa? Pessoa { get; set; }

        public ICollection<ProdutoPedido> ProdutosPedidos { get; set; }

    }
}
