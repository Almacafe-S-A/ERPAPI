using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class ColorsDetailQuotation
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ColorsDetailQuotationId { get; set; }
        [ForeignKey("ColorId")]
        public Int64 ColorId { get; set; }
        [Display(Name = "Codigo Cotizacion")]
        [ForeignKey("QuotationCode")]
        public Int64 QuotationCode { get; set; }
        [Display(Name = "Version")]
        public Int64 QuotationVersion { get; set; }
        [ForeignKey("QuotationDetailId")]
        public Int64 QuotationDetailId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
