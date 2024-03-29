﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class SolicitudCertificadoLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CertificadoLineId { get; set; }
        [Display(Name = "Certificado")]
        public Int64 IdSCD { get; set; }
        [Display(Name = "Producto")]

        public int ReciboId { get; set; }


        public Int64 SubProductId { get; set; }
        [ForeignKey("SubProductId")]
        public SubProduct Product { get; set; }
        [Display(Name = "Nombre producto")]
        public string SubProductName { get; set; }

        public int Pda { get; set; }

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

        [Display(Name = "Total")]
        public decimal Amount { get; set; }

        [Display(Name = "Total Cantidad")]
        public decimal TotalCantidad { get; set; }

        public Int64? GoodsReceivedLineId { get; set; }
        [ForeignKey("GoodsReceivedLineId")]
        public GoodsReceivedLine GoodsReceivedLine { get; set; }
    }
}
