using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Models;
using Api_Catalogo_Livros.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Catalogo_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class AutoresController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IAutoresRepository _repository;

        public AutoresController(AppDbContext context, IAutoresRepository repository)
        {
            _context = context;
            _repository = repository;
        }


        /// <summary>
        /// Obtem 1 lista de livros e seus autores
        /// </summary>
        /// <returns></returns>
        [HttpGet("autoreslivros")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Livros>> GetLivrosAutores()
        {

            var objet = _repository.GetLivrosAutores();
            return Ok(objet);

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
        public ActionResult<IEnumerable<Autores>> Get()
        {
            var objet = _repository.GetAutores();
            return Ok(objet);

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
        public ActionResult<Autores> GetById(int id)
        {

            var objet = _repository.GetById(id);

            if (objet is null)
            {
                return NotFound($"O Registro com ID: {id} não foi encontrado");
            }

            return Ok(objet);

        }

        //add 
        /// <summary>
        /// Adiciona 1 novo registro
        /// </summary>
        /// <param name="objet"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Post(Autores objet)
        {

            if (objet is null)
            {
                return BadRequest("Dados inválidos!");
            }

            var objetNovo = _repository.Create(objet);

            return new CreatedAtRouteResult("ObterAutor",
                new { id = objetNovo.AutorId }, objetNovo);
        }

        //update
        /// <summary>
        /// Atualiza 1 novo registro
        /// </summary>
        /// <param name="id"></param>
        /// <param name="objet"></param>
        /// <returns></returns>
        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Put(int id, Autores objet)
        {

            if (id != objet.AutorId)
            {
                return BadRequest("Dados inválidos!");
            }

            _repository.Update(objet);

            return Ok(objet);

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

            var objet = _repository.GetById(id);

            if (objet is null)
            {
                return NotFound($"O Registro com ID: {id} não foi encontrado");
            }

            var objetExcluido = _repository.Delete(id);
            return Ok(objetExcluido);

        }
    }
}
