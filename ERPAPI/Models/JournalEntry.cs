using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class JournalEntry
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 JournalEntryId { get; set; }

        public JournalEntry()
        {
          //  JournalEntryLines = new HashSet<JournalEntryLine>();
        }

        [Display(Name = "Id Libro Mayor")]
        public int? GeneralLedgerHeaderId { get; set; }
        [Display(Name = "Id de la Moneda")]
        public int CurrencyId { get; set; }



        [Display(Name = "Tipo de Socio de negocios")]
        public int PartyTypeId { get; set; }

        [Display(Name = "Tipo de Socio de negocios")]
        public string PartyTypeName { get; set; }

        [Display(Name = "Id Documento")]
        public Int64 DocumentId { get; set; }


        [Display(Name = "Id de Socio de negocios")]
        public int? PartyId { get; set; }

        [Display(Name = "Tipos de Voucher/Documento")]
        //  public JournalVoucherTypes? VoucherType { get; set; }
        public int? VoucherType { get; set; }


        [Display(Name = "Tipos de Voucher/Documento")]
        public string TypeJournalName { get; set; }

        [Display(Name = "Fecha de creacion")]
        public DateTime Date { get; set; }
        [Display(Name = "Fecha de Posteo")]
        public DateTime DatePosted { get; set; }

        public string Memo { get; set; }
        public string ReferenceNo { get; set; }
        public bool? Posted { get; set; }
        public virtual GeneralLedgerHeader GeneralLedgerHeader { get; set; }
        public virtual Party Party { get; set; }
        [Display(Name = "Id de Registro Pago")]
        public Int32 IdPaymentCode { get; set; }
        public Int32  IdTypeofPayment { get; set; }
        [Display(Name = "Estado")]
        public Int64? EstadoId { get; set; }
        [Display(Name = "Estado")]
        public string EstadoName { get; set; }

        [Display(Name = "Total Débito")]
        public double TotalDebit { get; set; }

        [Display(Name = "Total Crédito")]
        public double TotalCredit { get; set; }

        [Display(Name = "Tipo de ajuste")]
        public Int32 TypeOfAdjustmentId { get; set; }

        [Display(Name = "Tipo de ajuste")]
        public string TypeOfAdjustmentName { get; set; }

        public List<JournalEntryLine> JournalEntryLines { get; set; } = new List<JournalEntryLine>();
        [Required]
        [Display(Name = "Usuario de creacion")]
        public string CreatedUser { get; set; }
        [Required]
        [Display(Name = "Usuario de modificacion")]
        public string ModifiedUser { get; set; }
        [Required]
        [Display(Name = "Fecha de creacion")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Display(Name = "Fecha de Modificacion")]
        public DateTime ModifiedDate { get; set; }

    }


}
