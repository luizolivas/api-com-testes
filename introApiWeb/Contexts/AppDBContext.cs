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

        public DbSet<Pedido> Pedidos { get; set; }

        public DbSet<ProdutoPedido> ProdutoPedidos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Pedido>()
                .HasOne(pedido => pedido.Pessoa)
                .WithMany(pessoa => pessoa.Pedidos)
                .HasForeignKey(pedido => pedido.PessoaId);


        }


        //public DbSet<introApiWeb.Models.Pedido>? Pedido { get; set; }
    }
}
