using introApiWeb.Contexts;
using introApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace introApiWeb.Services
{
    public class ProdutoPedidoService : IProdutoPedidoService
    {
        private readonly AppDBContext _context;

        public ProdutoPedidoService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<ProdutoPedido>> getAllProdutoPedido()
        {
            return await _context.ProdutoPedidos.ToListAsync();
        }

        public async Task AddProdutoPedido(ProdutoPedido produtoPed)
        {
            // Certifique-se de que os IDs são válidos
            Produto? produto = await _context.Produtos.FindAsync(produtoPed.ProdutoId);
            Pedido? pedido = await _context.Pedidos.FindAsync(produtoPed.PedidoId);

            if (produto != null && pedido != null)
            {
                try
                {
                    produtoPed.Produto = produto;
                    produtoPed.Pedido = pedido;

                    _context.ProdutoPedidos.Add(produtoPed);
                    await _context.SaveChangesAsync();
                }
                catch { }

            }
            else
            {
                throw new Exception("Produto ou Pedido não encontrado");
            }
        }

        public async Task DeleteProdutoPedido(int produtoPedId)
        {
            var p = _context.ProdutoPedidos.Find(produtoPedId);

            if (p is null)
            {
                return;
            }

            _context.ProdutoPedidos.Remove(p);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProdutoPedido(ProdutoPedido p)
        {
            if (p is null)
            {
                return;
            }
            ProdutoPedido? pp = await _context.ProdutoPedidos.FindAsync(p.Id);

            //if (produtoPed == null)
            //{
            //    return;
            //}



            try
            {
                //_context.ProdutoPedidos.Update(produtoPed);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }


        }

        public List<ProdutoPedido> GetProdutosPedidosPorPedidoId(int pedidoId)
        {
            var produtosPedidos = _context.Pedidos
                .Where(p => p.Id == pedidoId)
                .Include(p => p.ProdutosPedidos)
                    .ThenInclude(pp => pp.Produto)
                .SelectMany(p => p.ProdutosPedidos)
                .ToList();

            return produtosPedidos;
        }

    }


}
