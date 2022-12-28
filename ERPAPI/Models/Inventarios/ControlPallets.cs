using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ControlPallets
    {
        [Display(Name = "Control Id")]
        public Int64 ControlPalletsId { get; set; }
        public string Motorista { get; set; }

        [Display(Name = "Sucursal")]
        public Int64 BranchId { get; set; }

        [Display(Name = "Sucursal")]
        public string BranchName { get; set; }

        [Display(Name = "Estado")]
        public Int64 IdEstado { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }    

        [Display(Name = "Bodega")]    
        public int? WarehouseId { get; set; }

        [Display(Name = "Bodega")]
        public string WarehouseName { get; set; }

        [Display(Name = "Fecha control de estiba")]
        public DateTime DocumentDate { get; set; }
        [Display(Name = "Producto")]
        public Int64? ProductId { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        [Display(Name = "Producto")]
        public string ProductName { get; set; }

        [Display(Name = "Producto Cliente")]
        public Int64? SubProductId { get; set; }
        [ForeignKey("SubProductId")]
        public SubProduct SubProduct { get; set; }
        [Display(Name = "Producto Cliente")]
        public string SubProductName { get; set; }

        public string Observaciones { get; set; }

        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }
        [Display(Name = "Descripción de producto")]
        public string DescriptionProduct { get; set; }
        [Display(Name = "Placas")]
        public string Placa { get; set; }
        public string Marca { get; set; }

        [Display(Name = "Unidad de medida")]
        public int? UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        public UnitOfMeasure unitOfMeasure { get; set; }

        [Display(Name = "Unidad de medida")]
        public string UnitOfMeasureName { get; set; }



        [Display(Name = "Id Control")]
        public int PalletId { get; set; }
        public int EsIngreso { get; set; }
        public int EsSalida { get; set; }
        public int SubTotal { get; set; }
        public double TotalSacos { get; set; }
        public int TotalSacosPolietileno { get; set; }

        public int TotalSacosYute { get; set; }
        public int SacosDevueltos { get; set; }
        public double QQPesoBruto { get; set; }
        public double Tara { get; set; }
        public double QQPesoNeto { get; set; }
        public double QQPesoFinal { get; set; }

        public bool? ProductoPesado { get; set; } 

        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string Impreso { get; set; }
        [Display(Name = "Boleta de peso")]
        public Int64 WeightBallot { get; set; }
        [NotMapped]
        [ForeignKey("WeightBallot")]
        public Boleto_Ent BoletaPeso { get; set; }


        [Display(Name = "Id Autorización")]
        public Int64 GoodsDeliveryAuthorizationId { get; set; }

        public List<ControlPalletsLine> _ControlPalletsLine { get; set; } = new List<ControlPalletsLine>();


        #region Not Mapped
        [NotMapped]
        public double taracamion { get; set; }
        [NotMapped]
        public double pesobruto { get; set; }
        [NotMapped]
        public double pesoneto { get; set; }
        [NotMapped]
        public double pesoneto2 { get; set; }
        [NotMapped]
        public Boleto_Ent boleto_Ent { get; set; }
        #endregion

    }
}
