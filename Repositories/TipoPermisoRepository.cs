using Microsoft.EntityFrameworkCore;
using PruebaBackend.Data;
using PruebaBackend.Models;

namespace PruebaBackend.Repositories
{
    public class TipoPermisoRepository : IRepository<TipoPermiso>
    {
        private readonly ApplicationDbContext _context;

        public TipoPermisoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(TipoPermiso tipoPermiso)
        {
            await _context.TipoPermisos.AddAsync(tipoPermiso);
        }

        public void Delete(TipoPermiso tipoPermiso)
        {
            _context.TipoPermisos.Remove(tipoPermiso);
        }

        public async Task<IEnumerable<TipoPermiso>> GetAllAsync()
        {
            return await _context.TipoPermisos.ToListAsync();
        }

        public async Task<TipoPermiso> GetByIdAsync(int id)
        {
            return await _context.TipoPermisos.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public void Update(TipoPermiso tipoPermiso)
        {
            _context.TipoPermisos.Update(tipoPermiso);
        }
    }
}
