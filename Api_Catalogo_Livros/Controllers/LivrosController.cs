using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Catalogo_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LivrosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("autores")]
        public async Task<ActionResult<IEnumerable<Livros>>> GetAutoresLivrosAsync()
        {
            try
            {
                return await _context.Livros.Include(p => p.Autores)
                    .Where(c => c.AutorId <= 10)
                    .AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Ocorreu um problema!!! " +
                   "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }

        [HttpGet("editoras")]
        public async Task<ActionResult<IEnumerable<Livros>>> GetEditorasLivrosAsync()
        {
            try
            {
                return await _context.Livros.Include(p => p.Editoras)
                          .Where(p => p.LivroId <= 10)
                          .AsNoTracking().ToListAsync();
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema!!! " +
                    "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }

        //método que retorna 1 lista 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Livros>>> GetAsync()
        {
            try
            {
                var livros = await _context.Livros.Take(5).AsNoTracking().ToListAsync();

                if (livros is null)
                {
                    return NotFound("Lista não encontrada!");
                }
                return livros;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Ocorreu um problema!!! " +
                   "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }


        //método que retorna livro por id
        [HttpGet("{id:int}", Name = "ObterLivro")]
        public ActionResult<Livros> Get(int id)
        {
            try
            {
                var livro = _context.Livros.AsNoTracking().FirstOrDefault(p => p.LivroId == id);
                if (livro is null)
                {
                    return NotFound("Livro não encontrado!");
                }
                return livro;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                     "Ocorreu um problema!!! " +
                     "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }

        //add Post
        [HttpPost]
        public ActionResult Add(Livros livro)
        {
            try
            {
                if (livro is null)
                {
                    return BadRequest();
                }
                _context.Livros.Add(livro);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterLivro",
                    new { id = livro.LivroId }, livro);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Ocorreu um problema!!! " +
                   "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }

        //update
        [HttpPut("{id:int}")]
        public ActionResult Edit(int id, Livros livro)
        {
            try
            {
                if (id != livro.LivroId)
                {
                    return BadRequest("Livro não encontrado!");
                }

                _context.Entry(livro).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(livro);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Ocorreu um problema!!! " +
                                   "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }

        //delete/excluir
        [HttpDelete("{id:int}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var livro = _context.Livros.FirstOrDefault(p => p.LivroId == id);
                if (livro is null)
                {
                    return NotFound("Livro não encontrado!");
                }

                _context.Livros.Remove(livro);
                _context.SaveChanges();

                return Ok(livro);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Ocorreu um problema!!! " +
                                   "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }
    }
}
