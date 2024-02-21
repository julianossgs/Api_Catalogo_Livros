using Api_Catalogo_Livros.Models;

namespace Api_Catalogo_Livros.Repositories.Interfaces
{
    public interface IEditorasRepository
    {
        List<Editoras> GetEditoras();
        Editoras Get(int id);
        void Add(Editoras editoras);
        void Update(Editoras editoras);
        void Delete(int id);
    }
}
