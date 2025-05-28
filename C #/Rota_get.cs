using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Routes
{
    public static class Rota_GET
    {
        // Método de extensão para registrar rotas GET
        public static void MapGetRoutes(this WebApplication app)
        {
            // Rota GET: Retorna todos os filmes
            app.MapGet("/api/filmes", async (AppDbContext context) =>
            {
                var filmes = await context.Filmes.ToListAsync();
                return filmes.Any() ? Results.Ok(filmes) : Results.NotFound("Nenhum filme encontrado.");
            });

            // Rota GET: Retorna um filme específico pelo ID
            app.MapGet("/api/filmes/{id}", async (int id, AppDbContext context) =>
            {
                var filme = await context.Filmes.FindAsync(id);
                return filme != null ? Results.Ok(filme) : Results.NotFound("Filme não encontrado.");
            });

            // Rota GET: Retorna os filmes recomendados, com base no gênero e na nota mínima
            app.MapGet("/api/filmes/recomendados", async (string? genero, double notaMinima, AppDbContext context) =>
            {
                var query = context.Filmes.AsQueryable();

                if (!string.IsNullOrEmpty(genero))
                    query = query.Where(f => f.Genero.ToLower() == genero.ToLower());

                var filmesRecomendados = await query.Where(f => f.Nota >= notaMinima).ToListAsync();

                return filmesRecomendados.Any() ? Results.Ok(filmesRecomendados) : Results.NotFound("Nenhum filme recomendado encontrado.");
            });
        }
    }
}
