using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PruebaBackend.Models;

namespace PruebaBackend.Data
{
    public class ApplicationDbContext : IdentityDbContext<Usuario, IdentityRole<int>, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        
        public DbSet<EstatusPermiso> EstatusPermisos { get; set; }
        public DbSet<TipoPermiso> TipoPermisos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TipoPermiso>().HasData(
                new TipoPermiso { Id = 1, Descripcion = "Enfermedad" },
                new TipoPermiso { Id = 2, Descripcion = "Diligencias" },
                new TipoPermiso { Id = 3, Descripcion = "Otros" }
            );

            modelBuilder.Entity<EstatusPermiso>().HasData(
                new EstatusPermiso { IdEstatusPermiso = 1, Descripcion = "Pendiente", Activo = true },
                new EstatusPermiso { IdEstatusPermiso = 2, Descripcion = "Aprobado", Activo = true },
                new EstatusPermiso { IdEstatusPermiso = 3, Descripcion = "Rechazado", Activo = true }
            );

        }
    }
}