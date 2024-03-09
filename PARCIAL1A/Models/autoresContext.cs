using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace PARCIAL1A.Models
{
    public class autoresContext :DbContext
    {

        public autoresContext(DbContextOptions<autoresContext>options) : base(options)
        {
        
        }  


        public DbSet<autores> autores { get; set; }

        public DbSet<AutorLibro> AutorLibro { get; set; }

        public DbSet<Libros> Libros { get; set; }

        public DbSet<Posts> Posts { get; set; }

    }
}
