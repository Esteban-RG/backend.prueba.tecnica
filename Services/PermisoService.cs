using PruebaBackend.Models;
using PruebaBackend.Repositories;

namespace PruebaBackend.Services
{
    public class PermisoService
    {
        private readonly IRepository<Permiso> _permisoRepository;

        public PermisoService(IRepository<Permiso> permisoRepository)
        {
            _permisoRepository = permisoRepository;
        }

        public async Task<Permiso> CreateAsync(Permiso permiso)
        {
            await _permisoRepository.AddAsync(permiso);
            await _permisoRepository.SaveChangesAsync();
            return permiso;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var permiso = await _permisoRepository.GetByIdAsync(id);
            if (permiso == null) return false;

            _permisoRepository.Delete(permiso);
            return await _permisoRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<Permiso>> GetAllAsync()
        {
            return await _permisoRepository.GetAllAsync();
        }

        public async Task<IEnumerable<Permiso>> GetMyAsync(int usuarioId)
        {
            var allPermisos = await _permisoRepository.GetAllAsync();
            return allPermisos.Where(p => p.UsuarioId == usuarioId);
        }

        public async Task<Permiso> GetByIdAsync(int id)
        {
            return await _permisoRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(Permiso permiso)
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
