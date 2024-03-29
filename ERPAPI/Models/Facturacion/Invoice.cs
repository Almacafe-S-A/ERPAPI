﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Invoice
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvoiceId { get; set; }
        public string InvoiceName { get; set; }
        [Display(Name = "Envio")]
        public int ShipmentId { get; set; }

        [Display(Name = "Cotizacion")]
        public int OrderId { get; set; }

        [Display(Name = "Punto de emisión")]
        public Int64 IdPuntoEmision { get; set; }

        [Display(Name = "Fecha de Factura")]
        public DateTime InvoiceDate { get; set; }

        public bool Exonerado { get; set; }

        public bool Exento { get; set; }

        public Int64? CuentaBancariaId { get; set; }
        [ForeignKey("CuentaBancariaId ")]
        public AccountManagement accountManagement { get; set; }

        public Int64 Bank { get; set; }

        public string BankName { get; set; }

        public string CuentaBancaria { get; set; }

        public int DiasVencimiento { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        public DateTime InvoiceDueDate { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        public DateTime ExpirationDate { get; set; }
        [Display(Name = "Tipo de Factura")]
        public int InvoiceTypeId { get; set; }

        [Display(Name = "Sucursal")]
        public string Sucursal { get; set; }

        [Display(Name = "Caja")]
        public string Caja { get; set; }


        public string Sinopsis { get; set; }

        [Display(Name = "Numero de Factura")]
        public string TipoDocumento { get; set; }

        [Display(Name = "Numero de Factura")]
        public string NumeroDEI { get; set; }

        [Display(Name = "Numero de inicio")]
        public string NoInicio { get; set; }

        [Display(Name = "Numero fin")]
        public string NoFin { get; set; }

        public string Rango { get; set; }

        [Display(Name = "Fecha Limite")]
        public DateTime FechaLimiteEmision { get; set; }

        [Display(Name = "Numero de Factura")]
        public string CAI { get; set; }

        [Display(Name = "Numero de orden de compra exenta")]
        public string NoOCExenta { get; set; }

        [Display(Name = "Numero de constancia de registro de exoneracion")]
        public string NoConstanciadeRegistro { get; set; }

        [Display(Name = "Numero de registro de la SAG")]
        public string NoSAG { get; set; }

        [Display(Name = "RTN")]
        public string RTN { get; set; }

        [Display(Name = "Telefono")]
        public string Tefono { get; set; }

        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Display(Name = "Sucursal")]
        public int BranchId { get; set; }

        [Display(Name = "Nombre Sucursal")]
        public string BranchName { get; set; }

        [Display(Name = "Customer")]
        public int CustomerId { get; set; }

        public long? CustomerContractId { get; set; }
        [ForeignKey("CustomerContractId")]
        public CustomerContract CustomerContract { get; set; }

        [Display(Name = "Nombre Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Id")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Nombre Producto")]
        public string ProductName { get; set; }

        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        [Display(Name = "Moneda")]
        public int? CurrencyId { get; set; }

        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [Display(Name = "Moneda tasa")]
        public decimal Currency { get; set; }

        [Display(Name = "Numero de referencia de cliente")]
        public string CustomerRefNumber { get; set; }
        [Display(Name = "Tipo de ventas")]
        public int SalesTypeId { get; set; }

        [Display(Name = "Observacion")]
        public string Remarks { get; set; }

        [Display(Name = "Monto")]
        public decimal Amount { get; set; }
        public decimal SubTotal { get; set; }

        [Display(Name = "Descuento")]
        public decimal Discount { get; set; }


        [Display(Name = "Impuesto")]
        public decimal Tax { get; set; }

        [Display(Name = "Impuesto 18%")]
        public decimal Tax18 { get; set; }


        [Display(Name = "Flete")]
        [Column(TypeName = "Money")]
        public decimal Freight { get; set; }

        [Display(Name = "Total exento")]
        [Column(TypeName = "Money")]
        public decimal TotalExento { get; set; }

        [Display(Name = "Total exonerado")]
        [Column(TypeName = "Money")]
        public decimal TotalExonerado { get; set; }

        [Display(Name = "Total Gravado")]
        [Column(TypeName = "Money")]
        public decimal TotalGravado { get; set; }

        [Display(Name = "Total Gravado 18%")]
        [Column(TypeName = "Money")]
        public decimal TotalGravado18 { get; set; }
        [Column(TypeName = "Money")]
        public decimal Total { get; set; }

        public decimal Saldo { get; set; }

        public decimal SaldoImpuesto { get; set; }


        public Int64? JournalEntryId { get; set; }
        [ForeignKey("JournalEntryId")]
        public JournalEntry JournalEntry { get; set; }

        public string TotalLetras { get; set; }

        public Int64? IdEstado { get; set; }

        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public string Impreso { get; set; }
        public List<InvoiceLine> InvoiceLine { get; set; } = new List<InvoiceLine>();

    }
}
