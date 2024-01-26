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
    public class PedidoController : ControllerBase
    {
        private readonly IPedidoService _PedidoService;
        private readonly IProdutoPedidoService _ProdutoPedidoService;
        private readonly IRabitMQProducer _RabitMQProducer;

        public PedidoController(IPedidoService Pedidoervice, IProdutoPedidoService produtoPedidoService, IRabitMQProducer rabitMQProducer)
        {
            _PedidoService = Pedidoervice;
            _ProdutoPedidoService = produtoPedidoService;
            _RabitMQProducer = rabitMQProducer;
        }



        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> GetAllPedido()
        {
            List<Pedido> Pedido = await _PedidoService.getAllPedido();
            return Ok(Pedido);
        }


        [HttpGet("{idPed}/produtosPedidos")]
        public ActionResult<List<ProdutoPedido>> GetAllProdutoPedByPedido(int idPed)
        {
            List<ProdutoPedido> ProdutoPedido =  _ProdutoPedidoService.GetProdutosPedidosPorPedidoId(idPed);
            return Ok(ProdutoPedido);
        }


        [HttpPost]
        public async Task<ActionResult> AddPedido(Pedido Pedido)
        {
            try
            {
                await _PedidoService.AddPedido(Pedido);
                _RabitMQProducer.SendProductMessage(Pedido, "Produto");
                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao adicionar Pedido: {ex.Message}");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePedido(int id)
        {
            try
            {
                await _PedidoService.DeletePedido(id);

                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao remover Pedido: {ex.Message}");
            }
        }

    }
}
