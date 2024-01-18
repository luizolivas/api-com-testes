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
        public async Task Post_CriandoPessoa()
        {
            var p = new Pessoa
            {
                Nome = "Test Pessoa",
                Tel = "123456789"
            };
            await pessoaService.AddPessoa(p);

            var pessoaInDb = await dbContext.Pessoas.FirstOrDefaultAsync();
            Assert.NotNull(pessoaInDb);
            Assert.Equal("Test Pessoa", pessoaInDb.Nome);
            Assert.Equal("123456789", pessoaInDb.Tel);
        }
    }
}
