using Api_Catalogo_Livros.Models;

namespace Api_Catalogo_Livros.Repositories.Interfaces
{
    public interface IAutoresRepository
    {
        List<Autores> GetAutoresLivros();

        Autores Get(int id);

        void Add(Autores autores);

        void Update(Autores autores);

        void Delete(int id);
    }
}
