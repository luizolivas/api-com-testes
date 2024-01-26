using introApiWeb.Contexts;
using introApiWeb.Models;
using introApiWeb.Services;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace introApiWeb.Application.Tests.Services
{
    public class PessoaServiceTests
    {
        private IPessoaService pessoaService;
        private AppDBContext dbContext;

        public PessoaServiceTests()
        {
            
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new AppDBContext(options);

            
            pessoaService = new PessoaService(dbContext);

        }

        [Fact]
        public async Task GetAllPessoa_DeveBuscarAllPessoas()
        {
            var pessoas = new List<Pessoa>
            {
                new Pessoa { Id = 1, Nome = "João", Tel = "(43) 0000-1111" },
                new Pessoa { Id = 2, Nome = "Maria", Tel = "(43) 0000-2222" },
            };


            foreach (var pessoa in pessoas)
            {
                await pessoaService.AddPessoa(pessoa);
            }

            var resultado = await pessoaService.GetAllPessoa();

            Assert.NotNull(resultado);
            Assert.Equal(pessoas.Count, resultado.Count);

            for(int i = 0; i < pessoas.Count; i++)
            {
                Assert.Equal(pessoas[i].Id, resultado[i].Id);
                Assert.Equal(pessoas[i].Nome, resultado[i].Nome);
                Assert.Equal(pessoas[i].Tel, resultado[i].Tel);
            }

            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task FindPessoaById_DeveBuscarPessoaPorID()
        {
            var p1 = new Pessoa
            {
                Nome = "Test Pessoa1",
                Tel = "(43) 0000-1111"
            };
            await pessoaService.AddPessoa(p1);

            var p2 = new Pessoa
            {
                Nome = "Test Pessoa2",
                Tel = "(43) 0000-2222"
            };
            await pessoaService.AddPessoa(p2);

            var p3 = new Pessoa
            {
                Nome = "Test Pessoa3",
                Tel = "(43) 0000-3333"
            };
            await pessoaService.AddPessoa(p3);

            Pessoa? pessoaEncontrada = await pessoaService.FindPessoaById(3);
            Assert.NotNull(pessoaEncontrada);
            Assert.Equal("Test Pessoa3", pessoaEncontrada.Nome);
            Assert.Equal("(43) 0000-3333", pessoaEncontrada.Tel);

            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task AddPessoa_DeveAdicionarPessoaAoBanco()
        {
            var p = new Pessoa
            {
                Nome = "Test Pessoa",
                Tel = "(43) 0000-0000"
            };
            await pessoaService.AddPessoa(p);

            var pessoaInDb = await dbContext.Pessoas.FirstOrDefaultAsync();
            Assert.NotNull(pessoaInDb);
            Assert.Equal("Test Pessoa", pessoaInDb.Nome);
            Assert.Equal("(43) 0000-0000", pessoaInDb.Tel);

            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task UpdatePessoa_DeveAtualizarPessoaNoBanco()
        {
            var p = new Pessoa
            {
                Nome = "Test Pessoa",
                Tel = "(43) 0000-0000"
            };

            await pessoaService.AddPessoa(p);

            // Atualizar a mesma pessoa
            p.Nome = "Luizzzzzzzzzzzzzzzzzzzzz";
            p.Tel = "(43) 2020-2121";

            await pessoaService.UpdatePessoa(p);

            var pessoaAtualizada = await dbContext.Pessoas.FirstOrDefaultAsync();
            Assert.NotNull(pessoaAtualizada);
            Assert.Equal("Luizzzzzzzzzzzzzzzzzzzzz", pessoaAtualizada.Nome);
            Assert.Equal("(43) 2020-2121", pessoaAtualizada.Tel);

            dbContext.Database.EnsureDeleted();
        }

        [Fact]
        public async Task DeletePessoa_DeveDeletarPessoaNoBanco()
        {
            var p = new Pessoa
            {
                Nome = "Test Pessoa",
                Tel = "(43) 0000-0000"
            };

            await pessoaService.AddPessoa(p);

            await pessoaService.DeletePessoa(p.Id);

            var pessoaDeleteada = await dbContext.Pessoas.FirstOrDefaultAsync();
            Assert.Null(pessoaDeleteada);

            dbContext.Database.EnsureDeleted();
        }

    }
}
