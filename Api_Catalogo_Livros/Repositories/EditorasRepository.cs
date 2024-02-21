using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Models;
using Api_Catalogo_Livros.Repositories.Interfaces;

namespace Api_Catalogo_Livros.Repositories
{
    public class EditorasRepository : IEditorasRepository
    {
        private readonly AppDbContext _context;

        public EditorasRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(Editoras editoras)
        {
            _context.Editoras.Add(editoras);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Editoras.Remove(Get(id));
            _context.SaveChanges();
        }

        public Editoras Get(int id)
        {
            return _context.Editoras.Find(id);
        }

        public List<Editoras> GetEditoras()
        {
            return _context.Editoras.OrderBy(c => c.EditoraId).ToList();
        }

        public void Update(Editoras editoras)
        {
            _context.Editoras.Update(editoras);
            _context.SaveChanges();
        }
    }
}
