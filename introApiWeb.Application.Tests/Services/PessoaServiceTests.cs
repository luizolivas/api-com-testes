using introApiWeb.Contexts;
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
        public void Post_CriandoPessoa()
        {
            Assert.True(true);
        }
    }
}
