using introApiWeb.Contexts;
using introApiWeb.Models;
using Microsoft.AspNetCore.Mvc;
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
            Pessoa? p = _context.Pessoas.Find(Pedido.PessoaId);
            if (p != null)
            {
                Pedido.Pessoa = p;
                _context.Pedidos.Add(Pedido);
                await _context.SaveChangesAsync();

                
            }
            else
            {
                throw new Exception("Pessoa não encontrada");
            }

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
