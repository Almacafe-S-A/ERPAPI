﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CertificadoLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CertificadoLineId { get; set; }
        public int? PdaNo { get; set; }
        [Display(Name = "Certificado")]
        public Int64 IdCD { get; set; }
        [Display(Name = "Producto")]
        public Int64 SubProductId { get; set; }
        [ForeignKey("SubProductId")]
        public SubProduct SubProduct { get; set; }
        [Display(Name = "Nombre producto")]
        public string SubProductName { get; set; }

        [Display(Name = "Unidad de medida")]
        public Int64 UnitMeasureId { get; set; }
        [Display(Name = "Unidad de medida")]
        public string UnitMeasurName { get; set; }

        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        [Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }
        [Display(Name = "Precio")]
        [Column(TypeName = "Money")]
        public decimal Price { get; set; }
        [Display(Name = "Porcentaje de Merma")]
        public decimal? Merma { get; set; }

       

        [Display(Name = "Total")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }


        [Display(Name = "Bodega")]
        public int WarehouseId { get; set; }
        //[ForeignKey("WarehouseId")]
        ///public Warehouse Warehouse { get; set; }
        [Display(Name = "Bodega")]
        public string WarehouseName { get; set; }


        [Display(Name = "Total Cantidad")]
        public decimal TotalCantidad { get; set; }

        public decimal? CantidadDisponibleAutorizar { get; set; }

        public decimal? Saldo { get; set; }

        [Display(Name = "Saldo endoso")]
        public decimal SaldoEndoso { get; set; }

        
        public string Observaciones { get; set; }

        public decimal? DerechosFiscales { get; set; }

        public decimal? ValorUnitarioDerechos { get; set; }

        
        public int? ReciboId { get; set; }
        public Int64? GoodsReceivedLineId { get; set; }

        [ForeignKey("GoodsReceivedLineId")]

        public GoodsReceivedLine GoodsReceivedLine { get; set; }


        public decimal? CantidadDisponible { get; set; }
    }

}
