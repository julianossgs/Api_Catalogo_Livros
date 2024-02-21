using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api_Catalogo_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EditorasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public EditorasController(AppDbContext context)
        {
            _context = context;
        }

        //método que retorna 1 lista 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Editoras>>> GetAsync()
        {
            try
            {
                var editoras = await _context.Editoras
                    .Take(5).AsNoTracking().ToListAsync();

                if (editoras is null)
                {
                    return NotFound("Lista não encontrada!");
                }
                return editoras;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Ocorreu um problema!!! " +
                                   "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }


        //método que retorna editora por id
        [HttpGet("{id:int}", Name = "ObterEditora")]
        public async Task<ActionResult<Editoras>> GetAsync(int id)
        {
            try
            {
                var editora = await _context.Editoras.AsNoTracking()
                    .FirstOrDefaultAsync(p => p.EditoraId == id);
                if (editora is null)
                {
                    return NotFound("Editora não encontrada!");
                }
                return editora;
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
        public ActionResult Add(Editoras editora)
        {
            try
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
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                                   "Ocorreu um problema!!! " +
                                   "Entre em contato com Suporte técnico (35)98898-1198");
            }

        }

        //update
        [HttpPut("{id:int}")]
        public ActionResult Edit(int id, Editoras editora)
        {
            try
            {
                if (id != editora.EditoraId)
                {
                    return BadRequest("Editora não encontrada!");
                }

                _context.Entry(editora).State = EntityState.Modified;
                _context.SaveChanges();
                return Ok(editora);
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
                var editora = _context.Editoras.FirstOrDefault(p => p.EditoraId == id);
                if (editora is null)
                {
                    return NotFound("Editora não encontrada!");
                }

                _context.Editoras.Remove(editora);
                _context.SaveChanges();

                return Ok(editora);
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
