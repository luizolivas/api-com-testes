using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace introApiWeb.Models
{
    public class ProdutoPedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto Produto { get; set; }

        

        public int PedidoId { get; set; }
        public Pedido Pedido { get; set; }
    }
}
