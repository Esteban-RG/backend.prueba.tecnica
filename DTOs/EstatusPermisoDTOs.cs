using PruebaBackend.Models;

namespace PruebaBackend.DTOs
{
    public class EstatusPermisoDTOs
    {
        public record EstatusPermisoDTO(
           int Id,
           string Descripcion
           );

        // Mappers
        public static EstatusPermisoDTO FromModel(Models.EstatusPermiso estatusPermiso) => new EstatusPermisoDTO(
            estatusPermiso.IdEstatusPermiso,
            estatusPermiso.Descripcion
            );

        public static EstatusPermiso ToModel(EstatusPermisoDTO estatusPermisoDTO) => new EstatusPermiso
        {
            IdEstatusPermiso = estatusPermisoDTO.Id,
            Descripcion = estatusPermisoDTO.Descripcion
        };
    }
}
