using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using introApiWeb.Contexts;
using introApiWeb.Models;
using introApiWeb.Services;

namespace introApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoPedidoController : ControllerBase
    {
        private readonly ProdutoPedidoService _ProdutoPedidoService;

        public ProdutoPedidoController(ProdutoPedidoService ProdutoPedidoervice)
        {
            _ProdutoPedidoService = ProdutoPedidoervice;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProdutoPedido>>> GetAllProdutoPedido()
        {
            List<ProdutoPedido> ProdutoPedido = await _ProdutoPedidoService.getAllProdutoPedido();
            return Ok(ProdutoPedido);
        }

        [HttpPost]
        public async Task<ActionResult> AddProdutoPedido(ProdutoPedido ProdutoPedido)
        {
            try
            {
                await _ProdutoPedidoService.AddProdutoPedido(ProdutoPedido);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao adicionar ProdutoPedido: {ex.Message}");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProdutoPedido(int id)
        {
            try
            {
                await _ProdutoPedidoService.DeleteProdutoPedido(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao remover ProdutoPedido: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProdutoPedido(ProdutoPedido newProdutoPedido)
        {
            try
            {
                await _ProdutoPedidoService.UpdateProdutoPedido(newProdutoPedido);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao atualizar ProdutoPedido: {ex.Message}");
            }
        }
    }
}
