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

    public class EditorasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEditorasRepository _repository;

        public EditorasController(AppDbContext context, IEditorasRepository repository)
        {
            _context = context;
            _repository = repository;
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
        public ActionResult<IEnumerable<Editoras>> Get()
        {

            var objet = _repository.GetEditoras();
            return Ok(objet);

        }


        /// <summary>
        /// Obtem 1 lista de editoras em ordem alfabética
        /// </summary>
        /// <returns></returns>
        [HttpGet("editoranome")]
        public ActionResult<IEnumerable<Editoras>> GetEditoraNome()
        {
            var objet = _repository.GetEditorasNome();
            return Ok(objet);
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
        public ActionResult<Editoras> Get(int id)
        {

            var objet = _repository.Get(id);

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
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesDefaultResponseType]
        public ActionResult Post(Editoras objet)
        {

            if (objet is null)
            {
                return BadRequest("Dados inválidos!");
            }

            var objetNovo = _repository.Create(objet);

            return new CreatedAtRouteResult("ObterEditora",
                new { id = objetNovo.EditoraId }, objetNovo);

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
        public ActionResult Put(int id, Editoras objet)
        {

            if (id != objet.EditoraId)
            {
                return BadRequest("Dados inválidos!");
            }

            _repository.Update(objet);

            return Ok(objet);

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

            var objet = _repository.Get(id);

            if (objet is null)
            {
                return NotFound($"O Registro com ID: {id} não foi encontrado");
            }

            var objetExcluido = _repository.Delete(id);
            return Ok(objetExcluido);

        }
    }
}
