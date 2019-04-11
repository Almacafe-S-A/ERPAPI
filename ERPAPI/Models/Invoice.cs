using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Invoice
    {
        public int InvoiceId { get; set; }
        [Display(Name = "Numero de Factura")]
        public string InvoiceName { get; set; }
        [Display(Name = "Envio")]
        public int ShipmentId { get; set; }
        [Display(Name = "Fecha de Factura")]
        public DateTime InvoiceDate { get; set; }
        [Display(Name = "Fecha de vencimiento")]
        public DateTime InvoiceDueDate { get; set; }
        [Display(Name = "Tipo de Factura")]
        public int InvoiceTypeId { get; set; }
    }
}
