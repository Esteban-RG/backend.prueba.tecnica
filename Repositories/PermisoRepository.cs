
using Microsoft.EntityFrameworkCore;
using PruebaBackend.Data;
using PruebaBackend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PruebaBackend.Repositories
{
    public class PermisoRepository : IRepository<Permiso>
    {
        private readonly ApplicationDbContext _context;
        
        public PermisoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Permiso permiso)
        {
            await _context.Permisos.AddAsync(permiso);
        }

        public void Delete(Permiso permiso)
        {
            _context.Permisos.Remove(permiso);
        }

        public async Task<IEnumerable<Permiso>> GetAllAsync()
        {
            return await _context.Permisos.Include(p => p.TipoPermiso).Include(p => p.UsuarioId).ToListAsync();
        }

        public async Task<Permiso> GetByIdAsync(int id)
        {
            return await _context.Permisos.Include(p => p.TipoPermiso).Include(p => p.UsuarioId).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(Permiso permiso)
        {
             _context.Permisos.Update(permiso);
        }
    }
}
