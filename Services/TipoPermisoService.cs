using PruebaBackend.Models;
using PruebaBackend.Repositories;

namespace PruebaBackend.Services
{
    public class TipoPermisoService : IService<TipoPermiso>
    {
        private readonly IRepository<TipoPermiso> _tipoPermisoRepository;

        public TipoPermisoService(IRepository<TipoPermiso> tipoPermisoRepository)
        {
            _tipoPermisoRepository = tipoPermisoRepository;
        }

        public async Task<TipoPermiso> CreatePermisoAsync(TipoPermiso tipoPermiso)
        {
            await _tipoPermisoRepository.AddAsync(tipoPermiso);
            await _tipoPermisoRepository.SaveChangesAsync();
            return tipoPermiso;
        }

        public async Task<bool> DeletePermisoAsync(int id)
        {
            var tipoPermiso = await _tipoPermisoRepository.GetByIdAsync(id);
            if(tipoPermiso == null) return false;

            _tipoPermisoRepository.Delete(tipoPermiso);
            await _tipoPermisoRepository.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<TipoPermiso>> GetAllPermisosAsync()
        {
            return await _tipoPermisoRepository.GetAllAsync();
        }

        public async Task<TipoPermiso> GetPermisoByIdAsync(int id)
        {
            return await _tipoPermisoRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdatePermisoAsync(TipoPermiso tipoPermiso)
        {
            var existingTipoPermiso = await _tipoPermisoRepository.GetByIdAsync(tipoPermiso.Id);
            if (existingTipoPermiso == null) return false;

            existingTipoPermiso.Descripcion = tipoPermiso.Descripcion;
            return await _tipoPermisoRepository.SaveChangesAsync();
        }
    }
}
