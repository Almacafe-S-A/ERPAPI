﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string Barcode { get; set; }
        public string Correlative { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }


        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }

        [Display(Name = "UOM")]
        public int? UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        public  UnitOfMeasure UnitOfMeasure { get; set; }
        public double DefaultBuyingPrice { get; set; } = 0.0;
        public double DefaultSellingPrice { get; set; } = 0.0;
        [Display(Name = "Sucursal")]
        public int BranchId { get; set; }
        [Display(Name = "Sucursal")]
        public string BranchName { get; set; }
        [ForeignKey("BranchId")]
        public  Branch Branch { get; set; }
        [Display(Name = "Currency")]
        public int CurrencyId { get; set; }

        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }
      

        public int? FundingInterestRateId { get; set; }
        [ForeignKey("FundingInterestRateId")]
        public FundingInterestRate FundingInterestRate { get; set; }

        public decimal? Prima { get; set; }

        public long? ProductTypeId { get; set; }

        [ForeignKey("ProductTypeId")]
        public ElementoConfiguracion ProductType { get; set; }




        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public string UsuarioModificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }

        public List<ProductRelation> ProductRelation { get; set; }
    }
}
