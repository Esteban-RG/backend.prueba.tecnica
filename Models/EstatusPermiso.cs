using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace PruebaBackend.Models
{
    [Index(nameof(Descripcion), IsUnique = true)]
    public class EstatusPermiso
    {
        [Key]
        public int IdEstatusPermiso { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public bool Activo { get; set; } = true;
    }
}
