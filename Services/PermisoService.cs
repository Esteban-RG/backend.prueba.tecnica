using Microsoft.AspNetCore.Identity;
using PruebaBackend.DTOs;
using PruebaBackend.Models;
using PruebaBackend.Repositories;




namespace PruebaBackend.Services
{
    public class PermisoService
    {
        private readonly IRepository<Permiso> _permisoRepository;
        private readonly UserManager<Usuario> _userManager;


        public PermisoService(IRepository<Permiso> permisoRepository, UserManager<Usuario> userManager)
        {
            _permisoRepository = permisoRepository;
            _userManager = userManager;
        }

        public async Task<Permiso> CreateAsync(PermisoDTOs.NewPermiso newPermiso)
        {
            if (newPermiso.UsuarioId != null) {
                var usuario = _userManager.Users.FirstOrDefault(u => u.Id == newPermiso.UsuarioId);
            }

            var permiso = PermisoDTOs.ToModel(newPermiso);

            await _permisoRepository.AddAsync(permiso);
            await _permisoRepository.SaveChangesAsync();

            permiso = await _permisoRepository.GetByIdAsync(permiso.Id);

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
            var myPermisos = await _permisoRepository.FindAsync(p => p.UsuarioId == usuarioId);
            return myPermisos;
        }

        public async Task<IEnumerable<Permiso>> GetPendientesAsync()
        {
            var pendingPermisos = await _permisoRepository.FindAsync(p => p.IdEstatusPermiso == 1);
            return pendingPermisos;
        }

        public async Task<Permiso> GetByIdAsync(int id)
        {
            return await _permisoRepository.GetByIdAsync(id);
        }

        public async Task<bool> UpdateAsync(PermisoDTOs.PermisoDTO dto)
        {
            var existingPermiso = await _permisoRepository.GetByIdAsync(dto.Id);
            if (existingPermiso == null) return false;

            existingPermiso = PermisoDTOs.ToModel(dto);

            return await _permisoRepository.SaveChangesAsync();
        }

        internal async Task<bool> ApproveAsync(int id)
        {
            var permiso = await _permisoRepository.GetByIdAsync(id);
            if (permiso == null) return false;
            permiso.IdEstatusPermiso = 2;
            permiso.FechaRevision = DateTime.UtcNow;
            return await _permisoRepository.SaveChangesAsync();
        }

        internal async Task<bool> DenyAsync(int id, string comment)
        {
            var permiso = await _permisoRepository.GetByIdAsync(id);
            if (permiso == null) return false;
            permiso.IdEstatusPermiso = 3;
            permiso.ComentariosSupervisor = comment;
            permiso.FechaRevision = DateTime.UtcNow;
            return await _permisoRepository.SaveChangesAsync();
        }

        internal async Task<IEnumerable<Permiso>> SearchPermisosAsync(int? tipoPermisoId, int? idEstatusPermiso)
        {
            if (tipoPermisoId == 0 && idEstatusPermiso == 0)
            {
                return await _permisoRepository.GetAllAsync();
            }

            if (tipoPermisoId != 0 && idEstatusPermiso == 0)
            {
                return await _permisoRepository.FindAsync(p => p.TipoPermisoId == tipoPermisoId);
            }
            else if (tipoPermisoId == 0 && idEstatusPermiso != 0)
            {
                return await _permisoRepository.FindAsync(p => p.IdEstatusPermiso == idEstatusPermiso);
            }
            else 
            { 
                return await _permisoRepository.FindAsync(p => p.TipoPermisoId == tipoPermisoId && p.IdEstatusPermiso == idEstatusPermiso);
            }
        }
    }
}
