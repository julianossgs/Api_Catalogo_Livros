using Api_Catalogo_Livros.Models;
using Microsoft.EntityFrameworkCore;

namespace Api_Catalogo_Livros.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Livros> Livros { get; set; }
        public DbSet<Autores> Autores { get; set; }
        public DbSet<Editoras> Editoras { get; set; }
    }
}
