using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace PruebaBackend.DTOs
{
    public class AuthDTO
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
