using Microsoft.EntityFrameworkCore;
using FilmesAPI.Models;

namespace FilmesAPI.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
    }
}
