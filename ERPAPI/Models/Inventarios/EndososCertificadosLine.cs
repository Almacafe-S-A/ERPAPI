using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class EndososCertificadosLine
    {
        [Display(Name = "Id Linea")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 EndososCertificadosLineId { get; set; }

        [Display(Name = "Id Endoso")]
        public Int64 EndososCertificadosId { get; set; }

        public int Pda { get; set; }

        [Display(Name = "Unidad de medida")]
        public Int64 UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Linea de certificado")]
        public Int64 CertificadoLineId { get; set; }

        [Display(Name = "Producto")]
        public Int64 SubProductId { get; set; }

        [Display(Name = "Producto")]
        public string SubProductName { get; set; }

        [Display(Name = "Cantidad")]
        public decimal Quantity { get; set; }

        [Display(Name = "Precio")]
        public decimal Price { get; set; }

        [Display(Name = "Valor endoso")]
        public decimal ValorEndoso { get; set; }
        [NotMapped]
        public decimal CantidadLiberacion { get; set; }
        [NotMapped]
        public decimal ValorLiberado { get; set; }

        public decimal Saldo { get; set; }

        public decimal? DerechosFiscales { get; set; }

        public decimal? ValorUnitarioDerechos { get; set; }


    }
}
