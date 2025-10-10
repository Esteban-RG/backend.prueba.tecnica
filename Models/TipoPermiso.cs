using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaBackend.Models
{
    public class TipoPermiso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Descripcion { get; set; } = null!; 

        public virtual ICollection<Permiso> Permisos { get; set; } = new List<Permiso>();
    }
}