using Api_Catalogo_Livros.Models;

namespace Api_Catalogo_Livros.Repositories.Interfaces
{
    public interface IAutoresRepository
    {
        IEnumerable<Autores> GetAutores();
        IEnumerable<Autores> GetAutoresLivros();
        IEnumerable<Livros> GetLivrosAutores();
        Autores GetById(int id);
        Autores Create(Autores objet);
        Autores Update(Autores objet);
        Autores Delete(int id);
    }
}
