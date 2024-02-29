using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Models;
using Api_Catalogo_Livros.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Catalogo_Livros.Repositories
{
    public class LivrosRepository : ILivrosRepository
    {
        private readonly AppDbContext _context;

        public LivrosRepository(AppDbContext context)
        {
            _context = context;
        }

        public Livros Create(Livros objet)
        {
            if (objet is null)
                throw new ArgumentNullException(nameof(objet));

            _context.Livros.Add(objet);
            _context.SaveChanges();
            return objet;
        }

        public Livros Delete(int id)
        {
            var objet = _context.Livros.Find(id);
            if (objet is null)
                throw new ArgumentNullException(nameof(objet));

            _context.Livros.Remove(objet);
            _context.SaveChanges();
            return objet;
        }

        public Livros Get(int id)
        {
            return _context.Livros.FirstOrDefault(c => c.LivroId == id);
        }

        public IEnumerable<Livros> GetLivros()
        {
            return _context.Livros
                   .AsNoTracking()
                   .ToList();
        }

        public IEnumerable<Livros> GetLivrosEditoras()
        {
            return _context.Livros.Include(p => p.Autores)
                .Include(p => p.Editoras)
                .Where(c => c.LivroId <= 10)
                .OrderBy(c => c.Nome)
                .AsNoTracking()
                .ToList();
        }

        public IEnumerable<Livros> GetLivrosPrecos()
        {
            return _context.Livros
                .AsNoTracking()
                .OrderBy(c => c.Preco)
                .ToList();
        }

        public Livros Update(Livros objet)
        {
            if (objet is null)
                throw new ArgumentNullException(nameof(objet));

            _context.Entry(objet).State = EntityState.Modified;
            _context.SaveChanges();
            return objet;
        }
    }
}
