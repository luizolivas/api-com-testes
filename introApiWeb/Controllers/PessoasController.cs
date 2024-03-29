﻿using System;
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
        public async Task<ActionResult<List<Pessoa>>> GetAllPessoas()
        {
            List<Pessoa> pessoas = await _pessoaService.getAllPessoa();
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
                return Ok();
            }catch (Exception ex)
            {
                return BadRequest($"Falha ao atualizar pessoa: {ex.Message}");
            }
        }


    }
}
