using Microsoft.EntityFrameworkCore;
using CrudPersonasDiego.Models;

namespace CrudPersonasDiego.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Persona> PersonasDiegoN { get; set; }
    }
}
