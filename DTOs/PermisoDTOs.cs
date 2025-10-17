using PruebaBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace PruebaBackend.DTOs
{
    public class PermisoDTOs
    {
        public record NewSolicitud(
            [Required] int IdTipoPermiso,
            [Required] DateTime FechaPermiso
            );

        public record NewPermiso(
            [Required] string NombreEmpleado,
            [Required] string ApellidosEmpleado,
            int? UsuarioId,
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

        public static Permiso ToModel(NewPermiso dto) => new Permiso
        {
            NombreEmpleado = dto.NombreEmpleado,
            ApellidosEmpleado = dto.ApellidosEmpleado,
            UsuarioId = dto.UsuarioId,
            TipoPermisoId = dto.IdTipoPermiso,
            FechaPermiso = dto.FechaPermiso,
            IdEstatusPermiso = 1 
        };

        public static NewPermiso ToNewPermiso(NewSolicitud solicitud, Usuario usuario) => new NewPermiso(
            NombreEmpleado: usuario.Nombre,
            ApellidosEmpleado: usuario.Apellidos,
            UsuarioId: usuario.Id,
            IdTipoPermiso: solicitud.IdTipoPermiso,
            FechaPermiso: solicitud.FechaPermiso
            );

    }
}