using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace introApiWeb.Models
{
    public class Pedido
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime DataPedido { get; set; }

        public int PessoaId { get; set; }

        [JsonIgnore]
        public Pessoa? Pessoa { get; set; }

        [JsonIgnore]
        public ICollection<ProdutoPedido> ProdutosPedidos { get; set; }

    }
}
