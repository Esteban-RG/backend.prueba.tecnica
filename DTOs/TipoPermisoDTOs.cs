using PruebaBackend.Models;
using System.ComponentModel.DataAnnotations;

namespace PruebaBackend.DTOs
{
    public class TipoPermisoDTOs
    {
        public record NewTipoPermiso(
            [Required] string Descripcion
            );

        public record TipoPermisoDTO(
            int Id,
            string Descripcion
            );


        // Mappers
        public static TipoPermisoDTO FromModel(TipoPermiso tipoPermiso) => new TipoPermisoDTO(
            tipoPermiso.Id,
            tipoPermiso.Descripcion
            );

        public static TipoPermiso ToModel(NewTipoPermiso newTipoPermiso) => new TipoPermiso
        {
            Descripcion = newTipoPermiso.Descripcion
        };

        public static TipoPermiso ToModel(TipoPermisoDTO tipoPermisoDTO) => new TipoPermiso
        {
            Id = tipoPermisoDTO.Id,
            Descripcion = tipoPermisoDTO.Descripcion
        };
    }
}
