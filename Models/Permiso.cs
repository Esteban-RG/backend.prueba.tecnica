using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PruebaBackend.Models
{
    public class Permiso
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string NombreEmpleado { get; set; } = null!; 

        [Required]
        public string ApellidosEmpleado { get; set; } = null!;

        [Required]
        public int TipoPermisoId { get; set; }

        [Required]
        public DateTime FechaPermiso { get; set; }

        [ForeignKey("TipoPermisoId")]
        public virtual TipoPermiso TipoPermiso { get; set; } = null!;
    }
}