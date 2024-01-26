using introApiWeb.Contexts;
using introApiWeb.Helpers;
using introApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace introApiWeb.Services
{
    public class PessoaService : IPessoaService
    {
        private readonly AppDBContext _context;

        public PessoaService(AppDBContext context)
        {
            _context = context;
        }

        public async Task<List<Pessoa>> GetAllPessoa()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task<Pessoa> FindPessoaById(int pessoaId)
        {
            try
            {
                return await _context.Pessoas.FindAsync(pessoaId);
            }
            catch
            {
                throw new Exception("Erro ao buscar pessoa por ID.");
            }

        }

        public async Task AddPessoa(Pessoa pessoa)
        {
            try
            {
                if (ValidaNumTel.verificaNum(pessoa.Tel))
                {
                    _context.Pessoas.Add(pessoa);
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Formato de telefone Incorreto");
                }


            }catch (Exception ex) 
            {
                throw new Exception("Dados incorretos: " + ex.Message);
            }

        }

        public async Task DeletePessoa(int pessoaId)
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

            

            if (ValidaNumTel.verificaNum(p.Tel))
            {
                pessoa.Tel = p.Tel;
            }
            else
            {
                throw new Exception("Número de telefone inválido");
            }

            try { 

                _context.Pessoas.Update(pessoa);
                await _context.SaveChangesAsync();
            }catch (Exception ex)
            {
                throw new Exception("Erro ao atualizar pessoa");

            }


        }

    }
}
