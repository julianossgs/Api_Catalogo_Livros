using Api_Catalogo_Livros.Models;

namespace Api_Catalogo_Livros.Repositories.Interfaces
{
    public interface ILivrosRepository
    {
        List<Livros> GetLivros();
        Livros Get(int id);
        void Add(Livros livros);
        void Update(Livros livros);
        void Delete(int id);
    }
}
