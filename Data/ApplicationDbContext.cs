using Microsoft.EntityFrameworkCore;
using ApiLaboratorio.Models;
using MySqlX.XDevAPI.Relational;

namespace ApiLaboratorio.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<User> Usuarios { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolUsuario> RolesUsuarios { get; set; }
        public DbSet<Laboratorio> Laboratorios { get; set; }
        public DbSet<Horario> Horarios { get; set; }
        public DbSet<Carrera> Carreras { get; set; }
        public DbSet<Materia> Materias { get; set; }
        public DbSet<AsignarMateria> AsignacionesMaterias { get; set; }
        public DbSet<Asignacion> Asignaciones { get; set; }
        public DbSet    <Facultad> Facultades { get; set; }
      

    }
}
