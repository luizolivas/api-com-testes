using introApiWeb.Contexts;
using introApiWeb.Models;

namespace introApiWeb.Services
{
    public class PessoaService
    {
        private readonly AppDBContext _context;

        public PessoaService(AppDBContext context)
        {
            _context = context;
        }

        public List<Pessoa> getAllPessoa()
        {
            return _context.Pessoas.ToList();
        }

        public void AddPessoa(Pessoa pessoa)
        {
            _context.Pessoas.Add(pessoa);
            _context.SaveChanges();

        }

        public void DeletePessoa(long pessoaId)
        {
            var p = _context.Pessoas.Find(pessoaId);

            if (p is null)
            {
                return;
            }

            _context.Pessoas.Remove(p);
            _context.SaveChanges();
        }

        public void UpdatePessoa(Pessoa p)
        {
            if(p is null)
            {
                return;
            }
            Pessoa pessoa = _context.Pessoas.Find(p.Id);

            long idPessoa = pessoa.Id;
            try
            {
                _context.Pessoas.Update(p);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            

        }


        
    }
}
