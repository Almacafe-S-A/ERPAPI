using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InventarioFisicoLine
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public int? WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }
        public string WarehouseName { get; set; }

        public int? No { get; set; }

        public int InventarioFisicoId { get; set; }
        [ForeignKey("InventarioFisicoId")]
        public InventarioFisico inventarioFisico { get; set; }

        public Int64 ProductoId { get; set; }
        [ForeignKey("ProductoId")]
        public SubProduct Product { get; set; }

        public int? UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]

        public UnitOfMeasure UnitOfMeasure { get; set; }

        public string ProductoNombre { get; set; }

        public decimal? FactorSacos { get; set; }

        public int? NSacos { get; set; }

        public decimal SaldoLibros { get; set; }

        public decimal InventarioFisicoCantidad{ get; set; }

        public decimal Diferencia { get; set; }

        public string Estiba { get; set; }

        public string Observacion { get; set; }
    }
}
