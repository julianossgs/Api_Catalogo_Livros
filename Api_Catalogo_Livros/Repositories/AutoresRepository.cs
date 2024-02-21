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

        //add
        public void Add(Autores autores)
        {
            _context.Autores.Add(autores);
            _context.SaveChanges();
        }

        //delete
        public void Delete(int id)
        {
            _context.Autores.Remove(Get(id));
            _context.SaveChanges();
        }

        //Get por id
        public Autores Get(int id)
        {
            return _context.Autores
                .Include(c => c.Livros)
                .FirstOrDefault();
        }

        //Lista
        public List<Autores> GetAutoresLivros()
        {
            return _context.Autores
                .Include(c => c.Livros)
                .OrderBy(c => c.AutorId).ToList();
        }

        //update
        public void Update(Autores autores)
        {
            _context.Remove(autores);
            _context.SaveChanges();
        }
    }
}
