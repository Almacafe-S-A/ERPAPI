using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class PurchaseOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string PONumber { get; set; }

        public string Title { get; set; }

        public long? EstadoId { get; set; }

        [ForeignKey("EstadoId")]
        public Estados Estados { get; set; }

        public long? POTypeId { get; set; }

        [ForeignKey("EstadoId")]
        public ElementoConfiguracion POType { get; set; }

        public int? BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public Int64? VendorId { get; set; }
        [ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }

        public string VendorName { get; set; }

        public DateTime DatePlaced { get; set; }

        public string CurrencyName { get; set; }

        public int? CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public string Terms { get; set; }

        public double? Freight { get; set; }

        public Int64? TaxId { get; set; }

        public string TaxName { get; set; }

        public decimal TaxRate { get; set; }

        [ForeignKey("TaxId")]
        public Tax Tax { get; set; }

        public long? ShippingTypeId { get; set; }

        [ForeignKey("ShippingTypeId")]
        public ElementoConfiguracion ShippingType { get; set; }

        

        public string ShippingTypeName { get; set; }

        public string Requisitioner { get; set; }

        public string Remarks { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }

        [Display(Name = "Usuario de creación")]
        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }

        [Display(Name = "Monto")]
        public double Amount { get; set; }
        public double SubTotal { get; set; }

        public double Discount { get; set; }

        [Display(Name = "Impuesto%")]
        public double TaxAmount { get; set; }

        [Display(Name = "Impuesto 18%")]
        public double Tax18 { get; set; }
        

        [Display(Name = "Total exento")]
        public double TotalExento { get; set; }

        [Display(Name = "Total exonerado")]
        public double TotalExonerado { get; set; }

        [Display(Name = "Total Gravado")]
        public double TotalGravado { get; set; }

        [Display(Name = "Total Gravado 18%")]
        public double TotalGravado18 { get; set; }

        public double Total { get; set; }

        public IEnumerable<PurchaseOrderLine> PurchaseOrderLines { get; set; }

        
    }

    public class PurchaseOrderDTO : PurchaseOrder
    {
        public Kardex Kardex { get; set; } = new Kardex();
    }
}
