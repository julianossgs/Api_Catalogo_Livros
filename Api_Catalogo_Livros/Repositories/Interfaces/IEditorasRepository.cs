using Api_Catalogo_Livros.Models;

namespace Api_Catalogo_Livros.Repositories.Interfaces
{
    public interface IEditorasRepository
    {
        IEnumerable<Editoras> GetEditoras();
        IEnumerable<Editoras> GetEditorasNome();
        Editoras Get(int id);
        Editoras Create(Editoras objet);
        Editoras Update(Editoras objet);
        Editoras Delete(int id);
    }
}
