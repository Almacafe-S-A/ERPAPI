using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class JournalEntryCanceled
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int CanceledJournalentryId { get; set; }

        public int ReverseJournalEntryId { get; set; }

        public string TypeJournalName { get; set; }   
        
        public Int64 DocumentId { get; set; }

        public int VoucherType { get; set; }
    }
}
