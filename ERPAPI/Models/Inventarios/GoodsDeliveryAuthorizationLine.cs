using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GoodsDeliveryAuthorizationLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id Linea")]
        public Int64 GoodsDeliveryAuthorizationLineId { get; set; }
        [Display(Name = "Autorizacion Id")]
        public Int64 GoodsDeliveryAuthorizationId { get; set; }

        public Int64 CertificadoLineId { get; set; }

        [Display(Name = "Número de certificado")]
        public Int64 NoCertificadoDeposito { get; set; }

        [Display(Name = "Producto cliente")]
        public Int64 SubProductId { get; set; }

        [Display(Name = "Producto cliente")]
        public string SubProductName { get; set; }

        [Display(Name = "Unidad de medida")]
        public Int64 UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Unidad de medida")]
        public Int64 WarehouseId { get; set; }

        [Display(Name = "Unidad de medida")]
        public string WarehouseName { get; set; }

        [Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }

        public decimal Saldo { get; set; }

        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Valor del certificado")]
        public decimal valorcertificado { get; set; }
        [Display(Name = "Valor financiado")]
        public decimal valorfinanciado { get; set; }

        [Display(Name = "Valor a pagar impuestos")]
        public decimal ValorImpuestos { get; set; }

        [Display(Name = "Saldo de producto")]
        public decimal SaldoProducto { get; set; }


    }
}
