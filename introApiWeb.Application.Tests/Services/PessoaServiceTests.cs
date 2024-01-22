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
        private PessoaService pessoaService;
        private AppDBContext dbContext;

        public PessoaServiceTests()
        {
            // Configuração do DbContext em memória
            var options = new DbContextOptionsBuilder<AppDBContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            dbContext = new AppDBContext(options);

            // Configuração do serviço
            pessoaService = new PessoaService(dbContext);

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

            p.Nome = "Luizzzzzzzzzzzzzzzzzzzzz";
            p.Tel = "(43) 2020-2121";

            //Pessoa pteste = await dbContext.Pessoas.FirstOrDefaultAsync();

            await pessoaService.UpdatePessoa(p);

            var pessoaAtualizada = await dbContext.Pessoas.FirstOrDefaultAsync();
            Assert.NotNull(pessoaAtualizada);
            Assert.Equal("Luizzzzzzzzzzzzzzzzzzzzz", pessoaAtualizada.Nome);
            Assert.Equal("(43) 2020-2121", pessoaAtualizada.Tel);
        }
    }
}
