using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ProformaInvoiceLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id Linea")]
        public Int64 ProformaLineId { get; set; }
        [Display(Name = "Proforma")]
        public int ProformaInvoiceId { get; set; }
        [Display(Name = "Cotizacion")]
        public ProformaInvoice ProformaInvoice { get; set; }
        [Display(Name = "Product Id")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Nombre Producto")]
        public string ProductName { get; set; }

        [Display(Name = "SubProducto")]
        public Int64 SubProductId { get; set; }

        [Display(Name = "Nombre SubProducto")]
        public string SubProductName { get; set; }

        [Display(Name = "Unidad de Medida")]
        public Int64 UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de Medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        [Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }
        [Display(Name = "Precio")]
        public decimal Price { get; set; }
        [Display(Name = "Monto")]
        public decimal Amount { get; set; }

        [Display(Name = "Porcentaje descuento")]
        public decimal DiscountPercentage { get; set; }

        [Display(Name = "Monto descuento")]
        public decimal DiscountAmount { get; set; }

        [Display(Name = "Bodega")]
        public Int64 WareHouseId { get; set; }

        [Display(Name = "Bodega")]
        public string WareHouseName { get; set; }

        [Display(Name = "Centro de costos")]
        public Int64 CostCenterId { get; set; }

        [Display(Name = "Centro de costos")]
        public string CostCenterName { get; set; }

        [Display(Name = "Subtotal")]
        public decimal SubTotal { get; set; }
         [Display(Name = "% Impuesto")]
        public decimal TaxPercentage { get; set; }

        [Display(Name = "Código Impuesto")]
        public Int64 TaxId { get; set; }

        [Display(Name = "Código Impuesto")]
        public string TaxCode { get; set; }

        [Display(Name = "Monto Impuesto")]
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }



    }
}
