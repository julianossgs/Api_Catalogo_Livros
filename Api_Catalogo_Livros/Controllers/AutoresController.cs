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
    public class AutoresController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AutoresController(AppDbContext context)
        {
            _context = context;
        }

        //Método que retorna Autores e seus livros
        /// <summary>
        /// Obter 1 lista de autores e seus livros
        /// </summary>
        /// <returns></returns>
        [HttpGet("livros")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Autores>>> GetAutoresLivrosAsync()
        {

            return await _context.Autores.Include(p => p.Livros)
                       .Where(p => p.AutorId <= 10)
                       .AsNoTracking().ToListAsync();

        }


        //método que retorna 1 lista 
        /// <summary>
        /// Retorna 1 lista
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesDefaultResponseType]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<IEnumerable<Autores>>> GetAsync()
        {

            var autores = await _context.Autores
                .Take(10).AsNoTracking().ToListAsync();

            if (autores is null)
            {
                return NotFound("Lista não encontrada!");
            }
            return Ok(autores);

        }

        //método que retorna autor por id
        /// <summary>
        /// Obter 1 registro por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "ObterAutor")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Autores>> GetAsync(int id)
        {

            var autor = await _context.Autores
                .Include(c => c.Livros)
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.AutorId == id);

            if (autor is null)
            {
                return NotFound("Autor não encontrado!");
            }
            return Ok(autor);

        }

        //add 
        /// <summary>
        /// Adiciona 1 novo registro
        /// </summary>
        /// <param name="autor"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Add(Autores autor)
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

        //update
        /// <summary>
        /// Atualiza 1 novo registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="autor"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Edit(int id, Autores autor)
        {

            if (id != autor.AutorId)
            {
                return BadRequest("Autor não encontrado!");
            }

            _context.Entry(autor).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(autor);

        }

        //delete/excluir
        /// <summary>
        /// Exclui 1 novo registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Delete(int id)
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
    }
}
