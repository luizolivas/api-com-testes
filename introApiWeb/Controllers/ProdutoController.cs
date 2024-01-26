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
using introApiWeb.RabbitMQ;

namespace introApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly IProdutoService _ProdutoService;
        private readonly IRabitMQProducer _rabitMQProducer;

        public ProdutoController(IProdutoService Produtoervice, IRabitMQProducer rabitMQProducer)
        {
            _ProdutoService = Produtoervice;
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> GetAllProduto()
        {
            List<Produto> Produto = await _ProdutoService.GetAllProduto();

            // envia os dados dos produtos inseridos para a fila e o consumidor estará ouvindo esses dados na fila
            _rabitMQProducer.SendProductMessage(Produto);

            return Ok(Produto);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindProdutoById(int id)
        {
            try
            {
                var produto = await _ProdutoService.FindProdutoById(id);

                if (produto != null)
                {
                    if (produto.Id == 2)
                    {
                        throw new Exception("Erro intencional para testar a mensagem morta.");
                    }

                    _rabitMQProducer.SendProductMessage(produto);
                    //_rabitMQProducer.SendProductMessage(produto);
                    return Ok(produto);
                }
                else
                {
                    return NotFound(); // Produto não encontrado
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao buscar produto: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddProduto(Produto Produto)
        {
            try
            {
                await _ProdutoService.AddProduto(Produto);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao adicionar Produto: {ex.Message}");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduto(int id)
        {
            try
            {
                await _ProdutoService.DeleteProduto(id);
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao remover Produto: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduto(Produto newProduto)
        {
            try
            {
                await _ProdutoService.UpdateProduto(newProduto);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao atualizar Produto: {ex.Message}");
            }
        }
    }
}
