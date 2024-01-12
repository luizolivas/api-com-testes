using introApiWeb.Contexts;
using introApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace introApiWeb.Services
{
    public class PessoaService
    {
        private readonly AppDBContext _context;

        public PessoaService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Pessoa>> getAllPessoa()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task AddPessoa(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

        }

        public async Task DeletePessoa(long pessoaId)
        {
            var p = _context.Pessoas.Find(pessoaId);

            if (p is null)
            {
                return;
            }

            _context.Pessoas.Remove(p);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePessoa(Pessoa p)
        {
            if(p is null)
            {
                return;
            }
            Pessoa? pessoa = await _context.Pessoas.FindAsync(p.Id);

            if(pessoa == null)
            {
                return;
            }

            pessoa.Nome = p.Nome;
            pessoa.Tel = p.Tel;

            try
            {
                _context.Pessoas.Update(pessoa);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {

            }
            

        }

    }
}
