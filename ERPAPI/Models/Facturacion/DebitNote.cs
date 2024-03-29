﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class DebitNote
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 DebitNoteId { get; set; }
        public string DebitNoteName { get; set; }

        [Display(Name = "Fiscal")]
        public bool Fiscal { get; set; }

        public Int64? JournalEntryId { get; set; }
        [ForeignKey("JournalEntryId")]
        public JournalEntry JournalEntry { get; set; }

        public decimal Saldo { get; set; }

        public string Descripcion { get; set; }

        public int DiasVencimiento { get; set; }

        public string TipoDocumento { get; set; }

        public int? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }


        [Display(Name = "Punto de emisión")]
        public Int64 IdPuntoEmision { get; set; }
       
        [Display(Name = "Fecha de Nota de débito")]
        public DateTime DebitNoteDate { get; set; }
        [Display(Name = "Fecha de vencimiento")]
        public DateTime? DebitNoteDueDate { get; set; }

        public string CuentaContableIngresosNombre { get; set; }
        public Int64? CuentaContableIngresosId { get; set; }
        [ForeignKey("CuentaContableIngresosId")]
        public Accounting CuentaContableIngresos { get; set; }

        public string CuentaContableDebitoNombre { get; set; }
        public Int64? CuentaContableDebitoId { get; set; }
        [ForeignKey("CuentaContableDebitoId")]
        public Accounting CuentaContableDebito { get; set; }

        public string RangoAutorizado { get; set; }

        public Int64? CuentaBancariaId { get; set; }
        [ForeignKey("CuentaBancariaId ")]
        public AccountManagement accountManagement { get; set; }

        public string NumeroDEIString { get; set; }

        public Int64 Bank { get; set; }

        public string BankName { get; set; }

        public string CuentaBancaria { get; set; }

        [Display(Name = "Sucursal")]
        public string Sucursal { get; set; }


        [Display(Name = "Número de Factura")]
        public string NumeroDEI { get; set; }

        [Display(Name = "Número de inicio")]
        public string NoInicio { get; set; }

        [Display(Name = "Número fin")]
        public string NoFin { get; set; }

        [Display(Name = "Fecha Limite de emisión")]
        public DateTime FechaLimiteEmision { get; set; }

        [Display(Name = "Número CAI")]
        public string CAI { get; set; }

        [Display(Name = "Número de orden de compra exenta")]
        public string NoOCExenta { get; set; }

        [Display(Name = "Número de constancia de registro de exoneracion")]
        public string NoConstanciadeRegistro { get; set; }

        [Display(Name = "Número de registro de la SAG")]
        public string NoSAG { get; set; }

        [Display(Name = "RTN")]
        public string RTN { get; set; }

        [Display(Name = "Teléfono ")]
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
        public Int64 CustomerId { get; set; }
        
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        [Display(Name = "Nombre Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Tipo de ventas")]
        public int SalesTypeId { get; set; }

        [Display(Name = "Observacion")]
        public string Remarks { get; set; }

        [Display(Name = "Monto")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Display(Name = "Sub total")]
        [Column(TypeName = "Money")]
        public decimal SubTotal { get; set; }

        [Display(Name = "Descuento")]
        [Column(TypeName = "Money")]
        public decimal Discount { get; set; }


        [Display(Name = "Impuesto")]
        [Column(TypeName = "Money")]
        public decimal Tax { get; set; }

        
        [Column(TypeName = "Money")]
        public decimal Total { get; set; }

        public string TotalLetras { get; set; }

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

        [Display(Name = "Usuario de modificación")]
        public string UsuarioModificacion { get; set; }



        public string RevisadoPor { get; set; }

        public DateTime? AprobadoEl { get; set; }

        public string AprobadoPor { get; set;}

        public DateTime? RevisadoEl { get; set; }












        


        public List<DebitNoteLine> DebitNoteLine { get; set; } = new List<DebitNoteLine>();

    }
}
