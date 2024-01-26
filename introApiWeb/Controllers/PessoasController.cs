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
    public class PessoasController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        private readonly IRabitMQProducer _rabitMQProducer;

        public PessoasController(IPessoaService pessoaService, IRabitMQProducer rabitMQProducer)
        {
            _pessoaService = pessoaService;
            _rabitMQProducer = rabitMQProducer;
        }

        [HttpGet]
        public async Task<ActionResult<List<Pessoa>>> GetAllPessoas()
        {
            List<Pessoa> pessoas = await _pessoaService.GetAllPessoa();
            _rabitMQProducer.SendProductMessage(pessoas, "Pessoa");
            return Ok(pessoas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindPessoaById(int id)
        {
            try
            {
                var produto = await _pessoaService.FindPessoaById(id);

                if (produto != null)
                {
                    return Ok(produto);
                }
                else
                {
                    return NotFound(); 
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Falha ao buscar produto: {ex.Message}");
            }
        }

        [HttpPost]
        public async Task<ActionResult> AddPessoa(Pessoa pessoa)
        {
            try
            {
                await _pessoaService.AddPessoa(pessoa);
                _rabitMQProducer.SendProductMessage(pessoa, "Pessoa");
                return Ok();

            }catch (Exception ex)
            {
                return BadRequest($"Falha ao adicionar pessoa: {ex.Message}");

            }


        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePessoa(int id)
        {
            try
            {
                await _pessoaService.DeletePessoa(id);
                return Ok();

            }catch (Exception ex)
            {
                return BadRequest($"Falha ao remover pessoa: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePessoa(Pessoa newPessoa)
        {
            try
            {
                await _pessoaService.UpdatePessoa(newPessoa);
                _rabitMQProducer.SendProductMessage(newPessoa, "Pessoa");
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest($"Falha ao atualizar pessoa: {ex.Message}");
            }
        }


    }
}
