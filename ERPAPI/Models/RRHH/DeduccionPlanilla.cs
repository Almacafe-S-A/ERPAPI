using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class DeduccionPlanilla
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long DetallePlanillaId { get; set; }

        [Required]
        public long DeduccionId { get; set; }

        [Required]
        public string NombreDeduccion { get; set; }

        [Required]
        public float Monto { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }

        [Required]
        public string UsuarioModificacion { get; set; }

        [Required]
        public string UsuarioCreacion { get; set; }
    }
}