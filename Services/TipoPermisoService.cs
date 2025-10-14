using PruebaBackend.Models;
using PruebaBackend.Repositories;

namespace PruebaBackend.Services
{
    public class TipoPermisoService
    {
        private readonly IRepository<TipoPermiso> _tipoPermisoRepository;

        public TipoPermisoService(IRepository<TipoPermiso> tipoPermisoRepository)
        {
            _tipoPermisoRepository = tipoPermisoRepository;
        }

        public async Task<TipoPermiso> CreateAsync(TipoPermiso tipoPermiso)
        {
            await _tipoPermisoRepository.AddAsync(tipoPermiso);
            await _tipoPermisoRepository.SaveChangesAsync();
            return tipoPermiso;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var tipoPermiso = await _tipoPermisoRepository.GetByIdAsync(id);
            if(tipoPermiso == null) return false;

            _tipoPermisoRepository.Delete(tipoPermiso);
            await _tipoPermisoRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TipoPermiso>> GetAllAsync()
        {
            return await _tipoPermisoRepository.GetAllAsync();
        }

        public async Task<TipoPermiso> GetByIdAsync(int id)
        {
            return await _tipoPermisoRepository.GetByIdAsync(id);
        }

        public Task<IEnumerable<TipoPermiso>> GetMyAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(TipoPermiso tipoPermiso)
        {
            var existingTipoPermiso = await _tipoPermisoRepository.GetByIdAsync(tipoPermiso.Id);
            if (existingTipoPermiso == null) return false;

            existingTipoPermiso.Descripcion = tipoPermiso.Descripcion;
            return await _tipoPermisoRepository.SaveChangesAsync();
        }

        
    }
}
