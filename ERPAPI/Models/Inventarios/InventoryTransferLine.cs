using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InventoryTransferLine
    {
        [Key]
        public int Id { get; set; }

        public int InventoryTransferId { get; set; }
        [ForeignKey("InventoryTransferId")]
        public InventoryTransfer InventoryTransfer { get; set; }

        public Int64 ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string ProductName { get; set; }

        public decimal QtyStock { get; set; }

        public decimal QtyOut { get; set; }

        public decimal QtyIn { get; set; }

        public decimal Cost { get; set; }

        public decimal Price { get; set; }

        public int UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

        public string UnitOfMeasureName { get; set; }




    }
}
