﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CreditNoteLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Linea Id")]
        public Int64 CreditNoteLineId { get; set; }
        [Display(Name = "Nota de crédito")]
        public int CreditNoteId { get; set; }

        [Display(Name = "Nota de crédito")]
        public CreditNote CreditNote { get; set; }

        [Display(Name = "Producto")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Nombre producto")]
        public string ProductName { get; set; }

        [Display(Name = "SubProducto")]
        public Int64 SubProductId { get; set; }

        [Display(Name = "Nombre SubProducto")]
        public string SubProductName { get; set; }

        [Display(Name = "Cuenta")]
        public Int64 AccountId { get; set; }

        [Display(Name = "Cuenta")]
        public string AccountName { get; set; }


        [Display(Name = "Unidad de Medida")]
        public Int64 UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de Medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }
          [Display(Name = "Cantidad")]
        public double Quantity { get; set; }
        [Display(Name = "Precio")]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        [Display(Name = "Monto")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        public Int64 WareHouseId { get; set; }
        [Display(Name = "Centro de costos")]
        public Int64 CostCenterId { get; set; }

        [Display(Name = "Centro de costos")]
        public string CostCenterName { get; set; }

        [Display(Name = "Porcentaje descuento")]
        public decimal DiscountPercentage { get; set; }
          [Display(Name = "Monto descuento")]
        [Column(TypeName = "Money")]
        public decimal DiscountAmount { get; set; }
          [Display(Name = "Subtotal")]
        [Column(TypeName = "Money")]
        public decimal SubTotal { get; set; }

        public decimal CreditValue { get; set; }

        public decimal SaldoPendiente { get; set; }


        [Display(Name = "% Impuesto")]
        public decimal TaxPercentage { get; set; }

        [Display(Name = "Código Impuesto")]
        public Int64 TaxId { get; set; }

        [Display(Name = "Código Impuesto")]
        public string TaxCode { get; set; }

        [Display(Name = "Monto Impuesto")]
        [Column(TypeName = "Money")]
        public decimal TaxAmount { get; set; }
        [Column(TypeName = "Money")]
        public decimal Total { get; set; }
    }
}
