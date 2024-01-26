using introApiWeb.Models;

namespace introApiWeb.Services
{
    public interface IPedidoService
    {
        Task<List<Pedido>> getAllPedido();
        Task AddPedido(Pedido Pedido);
        Task DeletePedido(int PedidoId);
        List<ProdutoPedido> GetProdutosPedidosPorPedidoId(int pedidoId);
    }
}
