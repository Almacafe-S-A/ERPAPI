using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class Quotation
    {
        [Display(Name = "Codigo Cotizacion")]
        [Key]
        public Int64 QuotationCode { get; set; }
        [Display(Name = "Version")]
        public Int64 QuotationVersion { get; set; }
        [Display(Name = "Tipo")]
        public ElementoConfiguracion Tipo { get; set; }
        public DateTime QuotationDate { get; set; }
        [Display(Name = "Codigo Vendedor")]
        [ForeignKey("IdEmpleado")]
        public long IdEmpleado { get; set; }
        [Display(Name = "Codigo Cliente")]
        [ForeignKey("CustomerId")]
        public Int64 CustomerId { get; set; }
        [Display(Name = "Nombre del cliente")]
        public string CustomerName { get; set; }
        [Display(Name = "Teléfono")]
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Representative { get; set; }
        public string BranchCode { get; set; }
        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }

        public List<QuotationDetail> QuotationDetail { get; set; } = new List<QuotationDetail>();
    }
}
