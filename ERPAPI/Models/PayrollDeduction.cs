using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class PayrollDeduction
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "PayrollDeductionId")]
        public Int64 PayrollDeductionId { get; set; }

        public long IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }

        [Display(Name = "Concepto")]
        public string ConceptName { get; set; }

        [Display(Name = "Monto")]
        public double Amount { get; set; }

        [Display(Name = "Cuotas")]
        public double Fees { get; set; }

        [Display(Name = "Fecha Inicio")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Fecha Final")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Usuario de creación")]
        public string UsuarioCreacion { get; set; }

        [Display(Name = "Usuario de modificación")]
        public string UsuarioModificacion { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime? FechaCreacion { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime? FechaModificacion { get; set; }
    }
}
