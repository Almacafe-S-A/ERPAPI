using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InventarioBodegaHabilitada
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? No { get; set; } 

        public int? WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        public string WarehouseName { get; set; }

        public int InventarioFisicoId { get; set; }
        [ForeignKey("InventarioFisicoId")]
        public InventarioFisico inventarioFisico { get; set; }

        public Int64 ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public SubProduct Product { get; set; }

        public string Descripcion  { get; set; }


        public string ProductoNombre { get; set; }


        public decimal Cantidad{ get; set; }

        public string Estiba { get; set; }


        public int? UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public string UnitOfMeasureName { get; set; }


        public decimal Factor { get; set; }

        public decimal Valor { get; set; }

        public decimal? FactorPergamino { get; set; }

        public decimal? ValorPergamino { get; set; }


        public decimal SaldoCertificado { get; set; }

        public decimal SaldoPendienteCertificar { get; set; }

        public string Observacion { get; set; }
    }
}
