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
            JournalEntryLines = new HashSet<JournalEntryLine>();
        }

        [Display(Name = "Id Libro Mayor")]
        public int? GeneralLedgerHeaderId { get; set; }
        public int? PartyId { get; set; }
        [Display(Name = "Tipos de Voucher")]
        public JournalVoucherTypes? VoucherType { get; set; }
        [Display(Name = "Fecha")]
        public DateTime Date { get; set; }
        public string Memo { get; set; }
        public string ReferenceNo { get; set; }
        public bool? Posted { get; set; }
        public virtual GeneralLedgerHeader GeneralLedgerHeader { get; set; }
        public virtual Party Party { get; set; }

        public virtual ICollection<JournalEntryLine> JournalEntryLines { get; set; }
        [Required]
        [Display(Name = "Usuario de creacion")]
        public string UsuarioCreacion { get; set; }
        [Required]
        [Display(Name = "Usuario de modificacion")]
        public string UsuarioModificacion { get; set; }
        [Required]
        [Display(Name = "Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [Display(Name = "Fecha de Modificacion")]
        public DateTime FechaModificacion { get; set; }

    }


}
