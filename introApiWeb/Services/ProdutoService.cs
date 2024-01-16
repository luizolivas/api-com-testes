using introApiWeb.Contexts;
using introApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace introApiWeb.Services
{
    public class ProdutoService
    {
        private readonly AppDBContext _context;

        public ProdutoService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Produto>> getAllProduto()
        {
            return await _context.Produtos.ToListAsync();
        }

        public async Task AddProduto(Produto Produto)
        {
            _context.Produtos.Add(Produto);
            await _context.SaveChangesAsync();

        }

        public async Task DeleteProduto(int ProdutoId)
        {
            var p = _context.Produtos.Find(ProdutoId);

            if (p is null)
            {
                return;
            }

            _context.Produtos.Remove(p);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateProduto(Produto p)
        {
            if (p is null)
            {
                return;
            }
            Produto? Produto = await _context.Produtos.FindAsync(p.Id);

            if (Produto == null)
            {
                return;
            }

            Produto.Nome = p.Nome;
            Produto.Preco = p.Preco;

            try
            {
                _context.Produtos.Update(Produto);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

            }


        }

    }
}
