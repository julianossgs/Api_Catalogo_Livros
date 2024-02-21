using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Catalogo_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class LivrosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public LivrosController(AppDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Obter os autores e seus livros
        /// </summary>
        /// <returns></returns>
        [HttpGet("autores/editoras")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Livros>>> GetAutoresLivrosAsync()
        {

            return await _context.Livros.Include(p => p.Autores)
                .Include(p => p.Editoras)
                .Where(c => c.LivroId <= 10)
                .AsNoTracking().ToListAsync();

        }


        //método que retorna 1 lista 
        /// <summary>
        /// Obter 1 lista
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Livros>>> GetAsync()
        {

            var livros = await _context.Livros.Take(5).AsNoTracking().ToListAsync();

            if (livros is null)
            {
                return NotFound("Lista não encontrada!");
            }
            return Ok(livros);

        }


        //método que retorna livro por id
        /// <summary>
        /// Obter 1 registro por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "ObterLivro")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Livros> Get(int id)
        {

            var livro = _context.Livros.AsNoTracking().FirstOrDefault(p => p.LivroId == id);
            if (livro is null)
            {
                return NotFound("Livro não encontrado!");
            }
            return Ok(livro);

        }

        //add Post
        /// <summary>
        /// Adiciona 1 novo registro
        /// </summary>
        /// <param name="livro"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Add(Livros livro)
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

        //update
        /// <summary>
        /// Atualiza 1 novo registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="livro"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Edit(int id, Livros livro)
        {

            if (id != livro.LivroId)
            {
                return BadRequest("Livro não encontrado!");
            }

            _context.Entry(livro).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(livro);

        }

        //delete/excluir
        /// <summary>
        /// Exclui 1 registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
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
    }
}
