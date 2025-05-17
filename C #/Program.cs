using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<AppDbContext>(opt =>
    opt.UseSqlite("Data Source=filmes.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();

    if (!context.Filmes.Any())
    {
        context.Filmes.AddRange(new List<Filme>
        {
            new Filme { Titulo = "O Poderoso Chefão", Genero = "Drama", Ano = 1972, Nota = 9.2 },
            new Filme { Titulo = "Vingadores: Ultimato", Genero = "Ação", Ano = 2019, Nota = 8.5 },
            new Filme { Titulo = "Titanic", Genero = "Romance", Ano = 1997, Nota = 7.8 },
            new Filme { Titulo = "Coringa", Genero = "Drama", Ano = 2019, Nota = 8.4 },
            new Filme { Titulo = "Interestelar", Genero = "Ficção", Ano = 2014, Nota = 8.6 },
            new Filme { Titulo = "Shrek", Genero = "Comédia", Ano = 2001, Nota = 7.9 },
            new Filme { Titulo = "Matrix", Genero = "Ficção", Ano = 1999, Nota = 8.7 },
            new Filme { Titulo = "Homem-Aranha", Genero = "Ação", Ano = 2002, Nota = 7.3 },
            new Filme { Titulo = "Avatar", Genero = "Ficção", Ano = 2009, Nota = 7.8 },
            new Filme { Titulo = "Toy Story", Genero = "Animação", Ano = 1995, Nota = 8.3 }
        });
        context.SaveChanges();
    }
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
