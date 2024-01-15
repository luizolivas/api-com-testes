using introApiWeb.Contexts;
using introApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace introApiWeb.Services
{
    public class PedidoService
    {
        private readonly AppDBContext _context;

        public PedidoService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Pedido>> getAllPedido()
        {
            return await _context.Pedidos.ToListAsync();
        }

        public async Task AddPedido(Pedido Pedido)
        {
            _context.Pedidos.Add(Pedido);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePedido(int PedidoId)
        {
            var p = _context.Pedidos.Find(PedidoId);

            if (p is null)
            {
                return;
            }

            _context.Pedidos.Remove(p);
            await _context.SaveChangesAsync();
        }


    }
}
