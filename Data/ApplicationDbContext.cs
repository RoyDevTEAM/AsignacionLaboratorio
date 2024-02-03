using Microsoft.EntityFrameworkCore;
using ApiLaboratorio.Models;
namespace ApiLaboratorio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        //public DbSet<Facultad> Facultades { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<Carrera> Carreras { get; set; }

       
    }
}
