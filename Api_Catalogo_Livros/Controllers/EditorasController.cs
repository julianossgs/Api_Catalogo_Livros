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

    public class EditorasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EditorasController(AppDbContext context)
        {
            _context = context;
        }

        //método que retorna 1 lista 
        /// <summary>
        /// Retorna 1 Lista
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesDefaultResponseType]
        public async Task<ActionResult<IEnumerable<Editoras>>> GetAsync()
        {

            var editoras = await _context.Editoras
                .Take(5).AsNoTracking().ToListAsync();

            if (editoras is null)
            {
                return NotFound("Lista não encontrada!");
            }
            return Ok(editoras);

        }


        //método que retorna editora por id
        /// <summary>
        /// Obter 1 registro por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}", Name = "ObterEditora")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Editoras>> GetAsync(int id)
        {

            var editora = await _context.Editoras.AsNoTracking()
                .FirstOrDefaultAsync(p => p.EditoraId == id);
            if (editora is null)
            {
                return NotFound("Editora não encontrada!");
            }
            return Ok(editora);

        }

        //add 
        /// <summary>
        /// Adiciona 1 novo registro
        /// </summary>
        /// <param name="editora"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Add(Editoras editora)
        {

            if (editora is null)
            {
                return BadRequest();
            }
            _context.Editoras.Add(editora);
            _context.SaveChanges();

            return new CreatedAtRouteResult("ObterEditora",
                new { id = editora.EditoraId }, editora);

        }

        //update
        /// <summary>
        /// Atualiza 1 novo registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="editora"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Edit(int id, Editoras editora)
        {

            if (id != editora.EditoraId)
            {
                return BadRequest("Editora não encontrada!");
            }

            _context.Entry(editora).State = EntityState.Modified;
            _context.SaveChanges();
            return Ok(editora);

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

            var editora = _context.Editoras.FirstOrDefault(p => p.EditoraId == id);
            if (editora is null)
            {
                return NotFound("Editora não encontrada!");
            }

            _context.Editoras.Remove(editora);
            _context.SaveChanges();

            return Ok(editora);

        }
    }
}
