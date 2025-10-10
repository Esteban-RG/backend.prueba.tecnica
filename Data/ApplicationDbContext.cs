using Microsoft.EntityFrameworkCore;
using PruebaBackend.Models;

namespace PruebaBackend.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<TipoPermiso> TipoPermisos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoPermiso>().HasData(
                new TipoPermiso { Id = 1, Descripcion = "Enfermedad" },
                new TipoPermiso { Id = 2, Descripcion = "Diligencias" },
                new TipoPermiso { Id = 3, Descripcion = "Otros" }
            );
        }
    }
}