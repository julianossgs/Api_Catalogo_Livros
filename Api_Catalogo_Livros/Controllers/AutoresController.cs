using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Catalogo_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AutoresController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("livros")]
        public async Task<ActionResult<IEnumerable<Autores>>> GetAutoresLivrosAsync()
        {
            try
            {
                return await _context.Autores.Include(p => p.Livros)
                           .Where(p => p.AutorId <= 5)
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
        public async Task<ActionResult<IEnumerable<Autores>>> GetAsync()
        {
            try
            {

                var autores = await _context.Autores.Take(5).AsNoTracking().ToListAsync();

                if (autores is null)
                {
                    return NotFound("Lista não encontrada!");
                }
                return autores;
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Ocorreu um problema!!! " +
                    "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }

        //método que retorna autor por id
        [HttpGet("{id:int}", Name = "ObterAutor")]
        public async Task<ActionResult<Autores>> GetAsync(int id)
        {
            try
            {
                var autor = await _context.Autores.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.AutorId == id);
                if (autor is null)
                {
                    return NotFound("Autor não encontrado!");
                }
                return autor;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                   "Ocorreu um problema!!! " +
                   "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }

        //add 
        [HttpPost]
        public ActionResult Add(Autores autor)
        {
            try
            {
                if (autor is null)
                {
                    return BadRequest("Erro ao adicionar registro!");
                }
                _context.Autores.Add(autor);
                _context.SaveChanges();

                return new CreatedAtRouteResult("ObterAutor",
                    new { id = autor.AutorId }, autor);
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
        public ActionResult Edit(int id, Autores autor)
        {
            try
            {
                if (id != autor.AutorId)
                {
                    return BadRequest("Autor não encontrado!");
                }

                _context.Entry(autor).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(autor);
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
                var autor = _context.Autores.FirstOrDefault(p => p.AutorId == id);
                if (autor is null)
                {
                    return NotFound("Autor não encontrado!");
                }

                _context.Autores.Remove(autor);
                _context.SaveChanges();

                return Ok(autor);
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
