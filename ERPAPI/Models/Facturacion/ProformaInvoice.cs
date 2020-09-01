using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ProformaInvoice
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ProformaId { get; set; }
        [Display(Name = "Número de Proforma")]
        public string ProformaName { get; set; }

        [Display(Name = "RTN")]
        public string RTN { get; set; }

        [Display(Name = "Telefono")]
        public string Tefono { get; set; }

        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Display(Name = "Sucursal")]
        public Int64 BranchId { get; set; }

        [Display(Name = "Sucursal Nombre")]
        public string BranchName { get; set; }

        [Display(Name = "Customer")]
        public Int64 CustomerId { get; set; }

        [Display(Name = "Nombre Cliente")]
        public string CustomerName { get; set; }

        //[Display(Name = "Certificado Deposito")]
      //  public Int64 IdCD { get; set; }

        [Display(Name = "Area Utilizada")]
        public Int64 CustomerAreaId { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Id")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Fecha de fin")]
        public DateTime? EndDate { get; set; }


        [Display(Name = "Nombre Producto")]
        public string ProductName { get; set; }

        [Display(Name = "Cotización Asociada")]
        public Int64 SalesOrderId { get; set; }

        [Display(Name = "Certificado depósito")]
        public Int64 CertificadoDepositoId { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Moneda")]
        public int CurrencyId { get; set; }

        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [Display(Name = "Moneda tasa")]
        public decimal Currency { get; set; }

        [Display(Name = "Numero de referencia de cliente")]
        public string CustomerRefNumber { get; set; }
        [Display(Name = "Tipo de ventas")]
        public int SalesTypeId { get; set; }
        public string Remarks { get; set; }

        [Display(Name = "Monto")]
        public decimal Amount { get; set; }
        public decimal SubTotal { get; set; }     

        public decimal Discount { get; set; }

        [Display(Name = "Impuesto%")]
        public decimal Tax { get; set; }

        [Display(Name = "Impuesto 18%")]
        public decimal Tax18 { get; set; }

        [Display(Name = "Flete")]
        public decimal Freight { get; set; }

        [Display(Name = "Total exento")]
        public decimal TotalExento { get; set; }

        [Display(Name = "Total exonerado")]
        public decimal TotalExonerado { get; set; }

        [Display(Name = "Total Gravado")]
        public decimal TotalGravado { get; set; }

        [Display(Name = "Total Gravado 18%")]
        public decimal TotalGravado18 { get; set; }

        public decimal Total { get; set; }

        [Display(Name = "Estado")]
        public Int64 IdEstado { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }

        [Display(Name = "Usuario de creación")]
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public string Impreso { get; set; }

        public List<ProformaInvoiceLine> ProformaInvoiceLine { get; set; } = new List<ProformaInvoiceLine>();
    }


    public class ProformaInvoiceDTO : ProformaInvoice
    {
        public Kardex Kardex { get; set; } = new Kardex();

        public Guid Identificador { get; set; } 
    }



}
