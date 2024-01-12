using introApiWeb.Models;
using introApiWeb.Services;
using Microsoft.AspNetCore.Mvc;

namespace introApiWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutoController : Controller
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController( ProdutoService produtoService) {

            _produtoService = produtoService;
        }


        [HttpGet]
        public ActionResult<List<Produto>> GetAllProdutos()
        {
            return _produtoService.GetAllProdutos();
            
        }

        [HttpPost]
        public ActionResult AddProduto(Produto produto)
        {
            try
            {
                _produtoService.AddProduto(produto);
                return Ok(); 
            }
            catch (Exception ex)
            {
                
                return BadRequest($"Falha ao adicionar produto: {ex.Message}");
            }
        }





        public IActionResult Index()
        {
            return View();
        }
    }
}
