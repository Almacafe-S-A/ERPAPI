using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class PagoISR
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long Periodo { get; set; }

        [Required]
        public long EmpleadoId { get; set; }

        [Required]
        public double TotalAnual { get; set; }

        [Required]
        public double PagoAcumulado { get; set; }

        [Required]
        public double Saldo { get; set; }

        [Required]
        public long EstadoId { get; set; }

        [ForeignKey("EstadoId")]
        public Estados Estado { get; set; }

        [ForeignKey("EmpleadoId")]
        public Employees Empleado { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }

        [Required]
        public string UsuarioModificacion { get; set; }

        [Required]
        public string UsuarioCreacion { get; set; }

    }

    public class PagosISRDTO : PagoISR
    {
        public decimal CuotaISR { get; set; }
    }
}
