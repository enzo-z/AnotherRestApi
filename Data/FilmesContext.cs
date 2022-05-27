using Microsoft.EntityFrameworkCore;
using WebApiTest.Models;

namespace WebApiTest.Data
{
    public class FilmesContext : DbContext
    {
        public FilmesContext(DbContextOptions options) : base(options)
        {
        }

        [Comment("Tabela filmes")]
        public DbSet<Filme> Filmes => Set<Filme>();
    }
}
