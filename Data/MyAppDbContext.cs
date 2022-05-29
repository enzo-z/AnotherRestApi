using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest.Data
{
    public class MyAppDbContext : DbContext
    {
        public MyAppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Endereco>()
                .HasOne(endereco => endereco.Cinema)
                .WithOne(cinema => cinema.Endereco)
                .HasForeignKey<Cinema>(cinema => cinema.EnderecoId);
        }

        [Comment("Tabela Filmes")]
        public DbSet<Filme> Filmes => Set<Filme>();

        [Comment("Tabela Cinemas")]
        public DbSet<Cinema> Cinemas => Set<Cinema>();

        [Comment("Tabela Enderecos")]
        public DbSet<Endereco> Enderecos=> Set<Endereco>();
    }
}
