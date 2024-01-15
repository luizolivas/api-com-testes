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
    public class PedidoController : ControllerBase
    {
        private readonly PedidoService _PedidoService;

        public PedidoController(PedidoService Pedidoervice)
        {
            _PedidoService = Pedidoervice;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pedido>>> GetAllPedido()
        {
            List<Pedido> Pedido = await _PedidoService.getAllPedido();
            return Ok(Pedido);
        }

        [HttpPost]
        public async Task<ActionResult> AddPedido(Pedido Pedido)
        {
            try
            {
                await _PedidoService.AddPedido(Pedido);
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
