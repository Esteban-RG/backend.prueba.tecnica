using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBackend.Models
{
    public class Usuario : IdentityUser<int>
    {
        [Required]
        public string Nombre { get; set; } = null!;
        [Required]
        public string Apellidos { get; set; } = null!;
        [Required]
        public int TipoRolId { get; set; }


        public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
    }
}
