﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CertificadoLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CertificadoLineId { get; set; }
        [Display(Name = "Certificado")]
        public Int64 IdCD { get; set; }
        [Display(Name = "Producto")]
        public Int64 SubProductId { get; set; }
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
        public decimal Price { get; set; }
        [Display(Name = "Porcentaje de Merma")]
        public decimal? Merma { get; set; }

        [Display(Name = "Total")]
        public decimal Amount { get; set; }


        [Display(Name = "Bodega")]
        public Int64 WarehouseId { get; set; }
        [Display(Name = "Bodega")]
        public string WarehouseName { get; set; }


        [Display(Name = "Total Cantidad")]
        public decimal TotalCantidad { get; set; }

        [Display(Name = "Saldo endoso")]
        public decimal SaldoEndoso { get; set; }

        [Display(Name = "Centro de costos")]
        public Int64 CenterCostId { get; set; }

        [Display(Name = "Centro de costos")]
        public string CenterCostName { get; set; }
    }
}
