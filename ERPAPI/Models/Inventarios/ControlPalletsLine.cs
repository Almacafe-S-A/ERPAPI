using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ControlPalletsLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Linea Id")]
        public Int64 ControlPalletsLineId { get; set; }

        public int? Linea { get; set; }
        [Display(Name = "Id")]
        public Int64 ControlPalletsId { get; set; }
        public int Alto { get; set; }
        public int Ancho { get; set; }
        public int Otros { get; set; }
        public double Totallinea { get; set; }
        [Display(Name = "Cantidad de Sacos Yute")]
        public int cantidadYute { get; set; }
        [Display(Name = "Cantidad de Sacos de Polietileno")]
        public int cantidadPoliEtileno { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

        public Int64? SubProductId { get; set; }
        [ForeignKey("SubProductId")]
        public SubProduct SubProduct { get; set; }

        public string SubProductName { get; set; }

        public int? UnitofMeasureId { get; set; }
        [ForeignKey("UnitofMeasureId")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public string UnitofMeasureName  { get; set; }

        public decimal? Qty { get; set; }


        public int? WarehouseId  { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        public string WarehouseName { get; set; }

        public string Observacion { get; set; }



        [Display(Name = "Centro de costos")]
        public Int64 CenterCostId { get; set; }
        [Display(Name = "Centro de costos")]
        public string CenterCostName { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
