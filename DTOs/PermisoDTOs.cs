using PruebaBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace PruebaBackend.DTOs
{
    public class PermisoDTOs
    {
        public record NewPermiso(
            [Required] int IdTipoPermiso,
            [Required] DateTime FechaPermiso
            );
        public record PermisoDTO(
            int Id,
            string NombreEmpleado,
            string ApellidosEmpleado,
            TipoPermisoDTOs.TipoPermisoDTO TipoPermiso,
            DateTime FechaPermiso,
            EstatusPermisoDTOs.EstatusPermisoDTO EstatusPermiso,
            UsuarioDTOs.UsuarioInfo? Usuario = null,
            string? ComentariosSupervisor = null,
            DateTime? FechaRevision = null
            );

        public record ApproveStatusDTO(
            string? ComentariosSupervisor = null
            );

        //Mappers

        public static PermisoDTO FromModel(Permiso permiso) => new PermisoDTO(
            permiso.Id,
            permiso.NombreEmpleado,
            permiso.ApellidosEmpleado,
            TipoPermisoDTOs.FromModel(permiso.TipoPermiso!),
            permiso.FechaPermiso,
            EstatusPermisoDTOs.FromModel(permiso.EstatusPermiso!),
            permiso.Usuario != null ? UsuarioDTOs.FromModel(permiso.Usuario) : null,
            permiso.ComentariosSupervisor,
            permiso.FechaRevision
            );


        public static Permiso ToModel(NewPermiso newPermiso) => new Permiso
        {
            TipoPermisoId = newPermiso.IdTipoPermiso,
            FechaPermiso = newPermiso.FechaPermiso
        };

        public static Permiso ToModel(PermisoDTO dto) => new Permiso
        {
            Id = dto.Id,
            NombreEmpleado = dto.NombreEmpleado,
            ApellidosEmpleado = dto.ApellidosEmpleado,
            IdEstatusPermiso = dto.EstatusPermiso.Id,
            TipoPermisoId = dto.TipoPermiso.Id,
            FechaPermiso = dto.FechaPermiso,
            ComentariosSupervisor = dto.ComentariosSupervisor,
            UsuarioId = dto.Usuario.Id,
            FechaRevision = dto.FechaRevision
        };
    }
}
