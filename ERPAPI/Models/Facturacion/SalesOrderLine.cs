using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class SalesOrderLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 SalesOrderLineId { get; set; }
        [Display(Name = "Sales Order")]
        public int SalesOrderId { get; set; }
        [Display(Name = "Cotizacion")]
        public SalesOrder SalesOrder { get; set; }

        [Display(Name = "Id")]
        public Int64 ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        [Display(Name = "Nombre Producto")]
        public string ProductName { get; set; }

        [Display(Name = "SubProducto")]
        public Int64 SubProductId { get; set; }
        [ForeignKey("SubProductId")]
        public SubProduct SubProduct { get; set; }

        [Display(Name = "Nombre SubProducto")]
        public string SubProductName { get; set; }

        [Display(Name = "Unidad de Medida")]
        public Int64 UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
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
          [Display(Name = "Subtotal")]


        public decimal? Valor { get; set; }

        public decimal? Porcentaje { get; set; }


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
