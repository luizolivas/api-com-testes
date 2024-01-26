using introApiWeb.Models;

namespace introApiWeb.Services
{
    public interface IProdutoPedidoService
    {
        Task<List<ProdutoPedido>> getAllProdutoPedido();
        Task AddProdutoPedido(ProdutoPedido produtoPed);
        Task DeleteProdutoPedido(int produtoPedId);
        Task UpdateProdutoPedido(ProdutoPedido p);
        List<ProdutoPedido> GetProdutosPedidosPorPedidoId(int pedidoId);

    }
}
