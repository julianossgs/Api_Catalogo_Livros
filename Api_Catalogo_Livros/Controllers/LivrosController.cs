using Api_Catalogo_Livros.Models;
using Api_Catalogo_Livros.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api_Catalogo_Livros.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    public class LivrosController : ControllerBase
    {

        private readonly ILivrosRepository _repository;

        public LivrosController(ILivrosRepository repository)
        {

            _repository = repository;
        }


        /// <summary>
        /// Obter os autores e seus livros
        /// </summary>
        /// <returns></returns>
        [HttpGet("autores/editoras")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Livros>> GetLivrosEditoras()
        {

            var objet = _repository.GetLivrosEditoras();

            return Ok(objet);

        }


        //método que retorna 1 lista 
        /// <summary>
        /// Obter 1 lista
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<IEnumerable<Livros>> Get()
        {

            var objet = _repository.GetLivros();

            return Ok(objet);

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

            var objet = _repository.Get(id);

            if (objet is null)
            {
                return NotFound($"O Registro com ID: {id} não foi encontrado");
            }

            return Ok(objet);

        }


        /// <summary>
        /// Obtem 1 lista de livros e seus preços
        /// </summary>
        /// <returns></returns>
        [HttpGet("livrosPrecos")]
        public ActionResult<IEnumerable<Livros>> GetLivrosPrecos()
        {
            var objet = _repository.GetLivrosPrecos();

            return Ok(objet);
        }

        //add Post
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
        public ActionResult Add(Livros objet)
        {

            if (objet is null)
            {
                return BadRequest("Dados inválidos!");
            }

            var objetNovo = _repository.Create(objet);

            return new CreatedAtRouteResult("ObterLivro",
                new { id = objetNovo.LivroId }, objetNovo);

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
        public ActionResult Edit(int id, Livros objet)
        {

            if (id != objet.LivroId)
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
