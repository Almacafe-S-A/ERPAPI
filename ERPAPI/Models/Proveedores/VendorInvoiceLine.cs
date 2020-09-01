using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class VendorInvoiceLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Linea Id")]
        public Int64 VendorInvoiceLineId { get; set; }
        [Display(Name = "Factura Proveedor")]
        public int VendorInvoiceId { get; set; }
        [Display(Name = "Factura Proveedor")]
        [ForeignKey("VendorInvoiceId")]
        public VendorInvoice VendorInvoice { get; set; }
        

        [Display(Name = "Nombre producto de Cosnumo/Servicio")]
        public string ItemName { get; set; }
        

        //[Display(Name = "Unidad de Medida")]
        //public Int64 UnitOfMeasureId { get; set; }

        //[Display(Name = "Unidad de Medida")]
        //public string UnitOfMeasureName { get; set; }

        public Int64? CostCenterId { get; set; }
        [ForeignKey("CostCenterId")]
        public CostCenter CostCenter { get; set; }

        public string CostCenterName { get; set; }


        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        //[Display(Name = "Cantidad")]
        //public decimal Quantity { get; set; }
        //[Display(Name = "Precio")]
        //public decimal Price { get; set; }
        [Display(Name = "Monto")]
        public decimal Amount { get; set; }
        

        //[Display(Name = "Porcentaje descuento")]
        //public decimal DiscountPercentage { get; set; }
        //[Display(Name = "Monto descuento")]
        //public decimal DiscountAmount { get; set; }
        //[Display(Name = "Subtotal")]
        //public decimal SubTotal { get; set; }
        [Display(Name = "% Impuesto")]
        public decimal TaxPercentage { get; set; }
        [Display(Name = "Código Impuesto")]
        public string TaxCode { get; set; }

        public Int64? TaxId { get; set; }
        [ForeignKey("TaxId")]
        public virtual Tax Tax { get; set; }

        [Display(Name = "Monto Impuesto")]
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }

        public long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Accounting Account { get; set; }

    }
}
