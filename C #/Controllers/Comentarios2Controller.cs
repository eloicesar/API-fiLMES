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

        // ✔️ Novo Endpoint: Buscar comentário por ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Comentario>> GetComentarioPorId(int id)
        {
            var comentario = await _context.Comentarios.FindAsync(id);

            if (comentario == null)
            {
                return NotFound();
            }

            return comentario;
        }

        // ✔️ Corrigido: POST com CreatedAtAction correto
        [HttpPost]
        public async Task<ActionResult<Comentario>> PostComentario(Comentario comentario)
        {
            _context.Comentarios.Add(comentario);
            await _context.SaveChangesAsync();

            return CreatedAtAction(
                nameof(GetComentarioPorId),
                new { id = comentario.Id },
                comentario);
        }

        // ✔️ Buscar comentários de um filme específico
        [HttpGet("filme/{filmeId}")]
        public async Task<ActionResult<IEnumerable<Comentario>>> GetComentariosPorFilme(int filmeId)
        {
            var comentarios = await _context.Comentarios
                .Where(c => c.FilmeId == filmeId)
                .ToListAsync();

            return comentarios;
        }
    }
}
