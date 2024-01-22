using introApiWeb.Contexts;
using introApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace introApiWeb.Services
{
    public class ProdutoPedidoService
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
            Pedido? ped = _context.Pedidos.Find(produtoPed.PedidoId);
            if (ped != null)
            {
                _context.ProdutoPedidos.Add(produtoPed);
                await _context.SaveChangesAsync();

            }
            else
            {
                throw new Exception("Pedido não encontrado");
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

    }
}
