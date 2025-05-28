using Microsoft.AspNetCore.Mvc;
using FilmesAPI.Data;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("api/comentarios")]
    public class ComentariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ComentariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentarios()
        {
            return await _context.Comentarios.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentarioPorId(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null) return NotFound();
            return comentario;
        }

        [HttpGet("filme/{filmeId}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentariosPorFilme(int filmeId)
        {
            var comentarios = await _context.Comentarios.Where(c => c.FilmeId == filmeId).ToListAsync();
            return comentarios;
        }

        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetComentarioPorId), new { id = comentario.Id }, comentario);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutComentario(int id, Comentario comentario)
        {
            if (id != comentario.Id) return BadRequest();

            _context.Entry(comentario).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ComentarioExists(id)) return NotFound();
                else throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComentario(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);
            if (comentario == null) return NotFound();

            _context.Comentarios.Remove(comentario);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool ComentarioExists(int id) => _context.Comentarios.Any(c => c.Id == id);
    }
}
