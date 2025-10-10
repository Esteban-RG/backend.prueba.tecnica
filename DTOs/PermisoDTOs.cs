using PruebaBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace PruebaBackend.DTOs
{
    public class PermisoDTOs
    {
        public record NewPermiso(
            [Required] string Nombre,
            [Required] string Apellidos,
            [Required] int IdTipoPermiso,
            [Required] DateTime Fecha
            );
        public record PermisoDTO(
            [Required] int Id,
            [Required] string Nombre,
            [Required] string Apellidos,
            [Required] TipoPermisoDTOs.TipoPermisoDTO TipoPermiso,
            [Required] DateTime Fecha
            );

        //Mappers

        public static PermisoDTO FromModel(Permiso permiso) => new PermisoDTO(
            permiso.Id,
            permiso.NombreEmpleado,
            permiso.ApellidosEmpleado,
            TipoPermisoDTOs.FromModel(permiso.TipoPermiso),
            permiso.FechaPermiso
            ); 

        public static Permiso ToModel(NewPermiso newPermiso) => new Permiso
        {
            NombreEmpleado = newPermiso.Nombre,
            ApellidosEmpleado = newPermiso.Apellidos,
            TipoPermisoId = newPermiso.IdTipoPermiso,
            FechaPermiso = newPermiso.Fecha
        };

        public static Permiso ToModel(PermisoDTO permisoDTO) => new Permiso
        {
            Id = permisoDTO.Id,
            NombreEmpleado = permisoDTO.Nombre,
            ApellidosEmpleado = permisoDTO.Apellidos,
            TipoPermisoId = permisoDTO.TipoPermiso.Id,
            FechaPermiso = permisoDTO.Fecha
        };
    }
}
