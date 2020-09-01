using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class DetallePlanilla
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long PlanillaId { get; set; }

        [Required]
        public long EmpleadoId { get; set; }

        [Required]
        public double MontoBruto { get; set; }

        [Required]
        public double TotalDeducciones { get; set; }

        [Required]
        public double MontoNeto { get; set; }

        [ForeignKey("DetallePlanillaId")]
        public List<DeduccionPlanilla> Deducciones { get; set; }
        
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
