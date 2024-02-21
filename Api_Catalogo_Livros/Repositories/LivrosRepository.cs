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

        public void Add(Livros livros)
        {
            _context.Livros.Add(livros);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            _context.Livros.Remove(Get(id));
            _context.SaveChanges();
        }

        public Livros Get(int id)
        {
            return _context.Livros
                .Include(c => c.Autores)
                .Include(c => c.Editoras)
                .FirstOrDefault();
        }

        public List<Livros> GetLivros()
        {
            return _context.Livros
                .Include(c => c.Autores)
                .Include(c => c.Editoras)
                .OrderBy(c => c.LivroId).ToList();
        }

        public void Update(Livros livros)
        {
            _context.Livros.Update(livros);
            _context.SaveChanges();
        }
    }
}
