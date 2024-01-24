using introApiWeb.Models;

namespace introApiWeb.Services
{
    public interface IProdutoService
    {
        Task<List<Produto>> GetAllProduto();
        Task<Produto> FindProdutoById(int produtoId);
        Task AddProduto(Produto produto);
        Task DeleteProduto(int produtoId);
        Task UpdateProduto(Produto produto);
    }
}
