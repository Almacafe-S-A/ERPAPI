/********************************************************************************************************
-- NAME   :  CRUDPurchaseOrderLine
-- PROPOSE:  show record PurchaseOrderLine
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
2.0                  09/12/2019          Marvin.Guillen                 Changes of Add fields TaxPercentage, TaxAmount ,DiscountAmount, DiscountPercentage,  SubTotal, Total, Amount
1.0                  27/09/2019          Carlos.Castillo                Creation of Model
********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class PurchaseOrderLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int? LineNumber { get; set; }

        public int PurchaseOrderId { get; set; }
        [ForeignKey("PurchaseOrderId")]
        public PurchaseOrder PurchaseOrder { get; set; }

        public Int64? ProductId { get; set; }

        public string ProductDescription { get; set; }

        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public decimal? QtyOrdered { get; set; }

        public decimal? QtyReceived { get; set; }

        public decimal? QtyReceivedToDate { get; set; }

        public decimal? Price { get; set; }

        public string TaxName { get; set; }

        public decimal TaxRate { get; set; }

        public Int64? TaxId { get; set; }

        [ForeignKey("TaxId")]
        public Tax Tax { get; set; }
        public decimal Amount { get; set; }
        public decimal TaxPercentage { get; set; }
        public decimal TaxAmount { get; set; }
        public int DiscountAmount { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        public int UnitOfMeasureId { get; set; }

        public string UnitOfMeasureName { get; set; }

        [ForeignKey("UnitOfMeasureId")]
        public UnitOfMeasure UnitOfMeasure { get; set; }

    }
}
