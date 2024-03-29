﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GoodsReceivedLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GoodsReceiveLinedId { get; set; }
        [Display(Name = "Recibo de mercaderia")]
        public Int64 GoodsReceivedId { get; set; }
        [ForeignKey("GoodsReceivedId")]
        public GoodsReceived GoodsReceived { get; set; }
        [Display(Name = "Unidad de Medida")]
        public Int64? UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de Medida")]
        public string UnitOfMeasureName { get; set; }


        [Display(Name = "Producto Cliente")]
        public Int64? SubProductId { get; set; }
        [ForeignKey("SubProductId")]
        public SubProduct SubProduct { get; set; }
        [Display(Name = "Producto Cliente")]
        public string SubProductName { get; set; }

        [Display(Name = "Descripcion del producto")]
        public string Description { get; set; }
        [Display(Name = "Estiba")]
        public Int64 ControlPalletsId { get; set; }
        [Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }
        [Display(Name = "Sacos")]
        public decimal? QuantitySacos { get; set; }

        public decimal? SaldoFisico { get; set; }

        public decimal? SaldoFisicoSacos { get; set; }

        [Display(Name = "Precio")]
        public decimal Price { get; set; }
        [Display(Name = "Total")]
        public decimal Total { get; set; }

        public decimal? SaldoporCertificar { get; set; }

        public int? Estiba { get; set; }


        [Display(Name = "Bodega")]
        public Int64 WareHouseId { get; set; }

        [Display(Name = "Bodega")]
        public string WareHouseName { get; set; }

        [Display(Name = "Centro de costos")]
        public Int64 CostCenterId { get; set; }

        public Int64? MaxKardexId { get; set; }
        [ForeignKey("MaxKardexId")]
        public Kardex Kardex { get; set; }




    }


    public class GoodsReceivedLineDTO
    {
        public Int64 GoodsReceiveLinedId { get; set; }

        [Display(Name = "Recibo de mercaderia")]
        public Int64 GoodsReceivedId { get; set; }
        [Display(Name = "Unidad de Medida")]
        public Int64 UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de Medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Producto")]
        public Int64 ProducId { get; set; }
        [Display(Name = "Producto")]
        public string ProductName { get; set; }

        [Display(Name = "Producto Cliente")]
        public Int64 SubProductId { get; set; }

        [Display(Name = "Producto Cliente")]
        public string SubProductName { get; set; }

        [Display(Name = "Descripcion del producto")]
        public string Description { get; set; }
        [Display(Name = "Estiba")]
        public Int64 ControlPalletsId { get; set; }
        [Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }
        [Display(Name = "Sacos")]
        public decimal QuantitySacos { get; set; }

        [Display(Name = "Precio")]
        public decimal Price { get; set; }
        [Display(Name = "Total")]
        public decimal Total { get; set; }
        [Display(Name = "Bodega")]
        public Int64 WareHouseId { get; set; }
        [Display(Name = "Centro de costos")]
        public Int64 CenterCostId { get; set; }

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }


}
