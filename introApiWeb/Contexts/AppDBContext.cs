using introApiWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace introApiWeb.Contexts
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
    : base(options)
        {
        }


        public DbSet<Pessoa> Pessoas { get; set; }

        public DbSet<Produto> Produtos { get; set; }
    }
}
