using introApiWeb.Models;

namespace introApiWeb.Services
{
    public interface IPessoaService
    {
        Task<List<Pessoa>> getAllPessoa();
        Task<Pessoa> FindPessoaById(int pessoaId);
        Task AddPessoa(Pessoa pessoa);
        Task DeletePessoa(int pessoaId);
        Task UpdatePessoa(Pessoa p);
    }
}
