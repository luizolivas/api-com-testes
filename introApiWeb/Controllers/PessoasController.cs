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
    public class PessoasController : ControllerBase
    {
        private readonly PessoaService _pessoaService;

        public PessoasController(PessoaService pessoaService)
        {
            _pessoaService = pessoaService;
        }

        [HttpGet]
        public ActionResult<List<Pessoa>> GetAllPessoas()
        {
            return _pessoaService.getAllPessoa();
        }

        [HttpPost]
        public ActionResult AddPessoa(Pessoa pessoa)
        {
            try
            {
                _pessoaService.AddPessoa(pessoa);
                return Ok();

            }catch (Exception ex)
            {
                return BadRequest($"Falha ao adicionar pessoa: {ex.Message}");

            }


        }

        [HttpDelete("{id}")]
        public ActionResult DeletePessoa(long id)
        {
            try
            {
                _pessoaService.DeletePessoa(id);
                return Ok();

            }catch (Exception ex)
            {
                return BadRequest($"Falha ao remover pessoa: {ex.Message}");
            }
        }


        [HttpPut("{id}")]
        public ActionResult UpdatePessoa(Pessoa newPessoa)
        {
            try
            {
                _pessoaService.UpdatePessoa(newPessoa);
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest($"Falha ao atualizar pessoa: {ex.Message}");
            }
        }
    }
}
