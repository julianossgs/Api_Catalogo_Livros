using Api_Catalogo_Livros.Models;

namespace Api_Catalogo_Livros.Repositories.Interfaces
{
    public interface ILivrosRepository
    {
        IEnumerable<Livros> GetLivros();
        IEnumerable<Livros> GetLivrosPrecos();
        IEnumerable<Livros> GetLivrosEditoras();
        Livros Get(int id);
        Livros Create(Livros livros);
        Livros Update(Livros livros);
        Livros Delete(int id);
    }
}
