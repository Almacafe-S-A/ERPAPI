using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class Deduction
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 DeductionId { get; set; }
        [Required]
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Codigo Tipo Deduccion")]
        public Int64 DeductionTypeId { get; set; }
        [Required]
        [Display(Name = "Tipo Deduccion")]
        public string DeductionType { get; set; }
        [Display(Name = "Formula")]
        public double Formula { get; set; }
        [Display(Name = "Quincena a pagar")]
        public double Fortnight { get; set; }
        [Required]
        public bool EsPorcentaje { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
