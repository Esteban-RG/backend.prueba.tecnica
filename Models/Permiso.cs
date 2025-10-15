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
        public int IdEstatusPermiso { get; set; } = 1;

        public string? ComentariosSupervisor { get; set; }

        public DateTime? FechaRevision { get; set; }

        public int? UsuarioId { get; set; }

        [Required]
        public DateTime FechaPermiso { get; set; }

        [ForeignKey("UsuarioId")]
        public virtual Usuario Usuario { get; set; } = null!;

        [ForeignKey("TipoPermisoId")]
        public virtual TipoPermiso TipoPermiso { get; set; } = null!;

        [ForeignKey("IdEstatusPermiso")]
        public virtual EstatusPermiso EstatusPermiso { get; set; } = null!;
    }
}