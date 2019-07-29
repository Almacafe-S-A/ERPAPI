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

        public int? GeneralLedgerHeaderId { get; set; }
        public int? PartyId { get; set; }
        public JournalVoucherTypes? VoucherType { get; set; }
        public DateTime Date { get; set; }
        public string Memo { get; set; }
        public string ReferenceNo { get; set; }
        public bool? Posted { get; set; }
        public virtual GeneralLedgerHeader GeneralLedgerHeader { get; set; }
        public virtual Party Party { get; set; }

        public virtual ICollection<JournalEntryLine> JournalEntryLines { get; set; }
    }


}
