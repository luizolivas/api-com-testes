using introApiWeb.Contexts;
using introApiWeb.Models;

namespace introApiWeb.Services
{
    public class ProdutoService
    {

        private readonly AppDBContext _context;

        public ProdutoService(AppDBContext context)
        {
            _context = context;
        }


        public List<Produto> GetAllProdutos()
        {
            return _context.Produtos.ToList();
        }

        public void AddProduto(Produto produto)
        {
            _context.Produtos.Add(produto);
            _context.SaveChanges();

        }

        public void DeleteProduto(int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto is null)
            {
                return;
            }
            _context.Produtos.Remove(produto); 
            _context.SaveChanges();
        }

        //public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

        //public static void Add(Pizza pizza)
        //{
        //    pizza.Id = nextId++;
        //    Pizzas.Add(pizza);
        //}

        //public static void Delete(int id)
        //{
        //    var pizza = Get(id);
        //    if (pizza is null)
        //        return;

        //    Pizzas.Remove(pizza);
        //}

        //public static void Update(Pizza pizza)
        //{
        //    var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
        //    if (index == -1)
        //        return;

        //    Pizzas[index] = pizza;
        //}
    }
}
