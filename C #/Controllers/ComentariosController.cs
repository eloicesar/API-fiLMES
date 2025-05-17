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

        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(null, new { id = comentario.Id }, comentario);
        }

        [HttpGet("filme/{filmeId}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentariosPorFilme(int filmeId)
        {
            return await _context.Comentarios
                .Where(c => c.FilmeId == filmeId)
                .ToListAsync();
        }
    }
}
