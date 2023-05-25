using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CustomerAcccountStatus
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        [Display(Name = "Factura")]
        public int? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }

        public Int64 DocumentoId { get; set; }

        public Int64? TipoDocumentoId { get; set; }
        [ForeignKey("TipoDocumentoId")]
        public TiposDocumento TiposDocumento { get; set; }

        public string TipoDocumento { get; set; }

        public string Sinopsis { get; set; }

        public int? InvoicePaymentId { get; set; }

        [ForeignKey("InvoicePaymentId")]
        public InvoicePayments InvoicePayments { get; set; }

        public string NoDocumento { get; set; }

        public decimal Debito { get; set; }

        public decimal Credito { get; set; }

        public decimal Saldo { get; set; }

        [Display(Name = "Customer")]
        public Int64 CustomerId { get; set; }

        [Display(Name = "Nombre Cliente")]
        public string CustomerName { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }









    }
}
