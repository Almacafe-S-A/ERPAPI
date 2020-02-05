using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class DeduccionEmpleado
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [Required]
        public long EmpleadoId { get; set; }

        [Required]
        public Int64 DeductionId { get; set; }

        [Required]
        public float Monto { get; set; }
        
        public DateTime VigenciaInicio { get; set; }

        public DateTime VigenciaFinaliza { get; set; }

        [Required]
        public int EstadoId { get; set; }
        
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }

        [ForeignKey("EmpeladoId")]
        public Employees Empleado { get; set; }

        [ForeignKey("DeductionId")]
        public Deduction Deduccion { get; set; }
    }
}
