using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Models;
using Api_Catalogo_Livros.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Catalogo_Livros.Repositories
{
    public class EditorasRepository : IEditorasRepository
    {
        private readonly AppDbContext _context;

        public EditorasRepository(AppDbContext context)
        {
            _context = context;
        }

        public Editoras Create(Editoras objet)
        {
            if (objet is null)
                throw new ArgumentNullException(nameof(objet));

            _context.Editoras.Add(objet);
            _context.SaveChanges();
            return objet;
        }

        public Editoras Delete(int id)
        {
            var objet = _context.Editoras.Find(id);
            if (objet is null)
                throw new ArgumentNullException(nameof(objet));

            _context.Editoras.Remove(objet);
            _context.SaveChanges();
            return objet;
        }

        public Editoras Get(int id)
        {
            return _context.Editoras.FirstOrDefault(c => c.EditoraId == id);
        }

        public IEnumerable<Editoras> GetEditoras()
        {
            return _context.Editoras
                   .AsNoTracking()
                   .ToList();
        }

        public IEnumerable<Editoras> GetEditorasNome()
        {
            return _context.Editoras
                    .AsNoTracking()
                    .OrderBy(c => c.Nome)
                    .ToList();
        }

        public Editoras Update(Editoras objet)
        {
            if (objet is null)
                throw new ArgumentNullException(nameof(objet));

            _context.Entry(objet).State = EntityState.Modified;
            _context.SaveChanges();
            return objet;
        }
    }
}
