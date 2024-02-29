using Api_Catalogo_Livros.Context;
using Api_Catalogo_Livros.Models;
using Api_Catalogo_Livros.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api_Catalogo_Livros.Repositories
{
    public class AutoresRepository : IAutoresRepository
    {
        private readonly AppDbContext _context;

        public AutoresRepository(AppDbContext context)
        {
            _context = context;
        }

        public Autores Create(Autores objet)
        {
            if (objet is null)
                throw new ArgumentNullException(nameof(objet));

            _context.Autores.Add(objet);
            _context.SaveChanges();
            return objet;
        }

        public Autores Delete(int id)
        {
            var objet = _context.Autores.Find(id);
            if (objet is null)
                throw new ArgumentNullException(nameof(objet));

            _context.Autores.Remove(objet);
            _context.SaveChanges();
            return objet;
        }

        public Autores GetById(int id)
        {
            return _context.Autores.FirstOrDefault(c => c.AutorId == id);
        }

        public IEnumerable<Autores> GetAutores()
        {
            return _context.Autores
                   .AsNoTracking()
                   .OrderBy(c => c.Nome)
                   .ToList();
        }

        public Autores Update(Autores objet)
        {
            if (objet is null)
                throw new ArgumentNullException(nameof(objet));

            _context.Entry(objet).State = EntityState.Modified;
            _context.SaveChanges();
            return objet;
        }

        public IEnumerable<Autores> GetAutoresLivros()
        {
            return _context.Autores.Include(p => p.Livros)
                      .Where(p => p.AutorId <= 10)
                      .AsNoTracking()
                      .ToList();
        }

        public IEnumerable<Livros> GetLivrosAutores()
        {
            return _context.Livros.Include(p => p.Autores)
                       .Where(p => p.LivroId <= 10)
                       .AsNoTracking()
                       .ToList();
        }
    }
}
