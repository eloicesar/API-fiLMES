using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/filmes")]
    public class FilmesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FilmesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Filme>>> GetFilmes()
        {
            return await _context.Filmes.Include(f => f.Comentarios).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Filme>> GetFilme(int id)
        {
            var filme = await _context.Filmes.Include(f => f.Comentarios).FirstOrDefaultAsync(f => f.Id == id);
            if (filme == null) return NotFound();
            return filme;
        }

        [HttpPost]
        public async Task<ActionResult<Filme>> PostFilme(Filme filme)
        {
            _context.Filmes.Add(filme);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFilme), new { id = filme.Id }, filme);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFilme(int id)
        {
            var filme = await _context.Filmes.FindAsync(id);
            if (filme == null) return NotFound();

            _context.Filmes.Remove(filme);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet("recomendados")]
        public async Task<ActionResult<IEnumerable<Filme>>> GetRecomendados(string? genero = null, double notaMinima = 8.0)
        {
            var query = _context.Filmes.AsQueryable();
            if (!string.IsNullOrEmpty(genero))
                query = query.Where(f => f.Genero.ToLower() == genero.ToLower());

            return await query.Where(f => f.Nota >= notaMinima).ToListAsync();
        }
    }
}
