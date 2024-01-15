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

        public async Task AddProdutoPedido(ProdutoPedido pessoa)
        {
            try
            {
                _context.ProdutoPedidos.Add(pessoa);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {

            }

        }

        public async Task DeleteProdutoPedido(int pessoaId)
        {
            var p = _context.ProdutoPedidos.Find(pessoaId);

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

            //if (pessoa == null)
            //{
            //    return;
            //}



            try
            {
                //_context.ProdutoPedidos.Update(pessoa);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }


        }

    }
}
