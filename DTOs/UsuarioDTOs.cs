using PruebaBackend.Models;

namespace PruebaBackend.DTOs
{
    public class UsuarioDTOs
    {
        public record UsuarioInfo(
            int Id,
            string Nombre,
            string Apellidos,
            string Username,
            string Email
            );

        // Mapper
        public static UsuarioInfo FromModel(Usuario usuario) => new UsuarioInfo(
            usuario.Id,
            usuario.Nombre,
            usuario.Apellidos,
            usuario.UserName!,
            usuario.Email!
            );

        
    }
}
