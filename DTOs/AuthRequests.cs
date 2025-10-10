using System.ComponentModel.DataAnnotations;

namespace PruebaBackend.DTOs
{
    public class AuthRequests
    {
        public record Login(
            [Required] string email,
            [Required] string Password
            );

        public record Register(
            [Required] string Nombre,
            [Required] string Apellidos,
            [Required] string Username,
            [Required] string Email,
            [Required] string Password
            );
    }
}
