using PruebaBackend.Models;
using PruebaBackend.Repositories;

namespace PruebaBackend.Services
{
    public class PermisoService : IService<Permiso>
    {
        private readonly IRepository<Permiso> _permisoRepository;

        public PermisoService(IRepository<Permiso> permisoRepository)
        {
            _permisoRepository = permisoRepository;
        }

        public async Task<Permiso> CreatePermisoAsync(Permiso permiso)
        {
            await _permisoRepository.AddAsync(permiso);
            await _permisoRepository.SaveChangesAsync();
            return permiso;
        }

        public async Task<bool> DeletePermisoAsync(int id)
        {
            var permiso = await _permisoRepository.GetByIdAsync(id);
            if (permiso == null) return false;

            _permisoRepository.Delete(permiso);
            return await _permisoRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Permiso>> GetAllPermisosAsync()
        {
            return await _permisoRepository.GetAllAsync();
        }

        public async Task<Permiso> GetPermisoByIdAsync(int id)
        {
            return await _permisoRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdatePermisoAsync(Permiso permiso)
        {
            var existingPermiso = await _permisoRepository.GetByIdAsync(permiso.Id);
            if (existingPermiso == null) return false;

            existingPermiso.NombreEmpleado = permiso.NombreEmpleado;
            existingPermiso.ApellidosEmpleado = permiso.ApellidosEmpleado;
            existingPermiso.TipoPermisoId = permiso.TipoPermisoId;
            existingPermiso.FechaPermiso = permiso.FechaPermiso;

            return await _permisoRepository.SaveChangesAsync();
        }
    }
}
