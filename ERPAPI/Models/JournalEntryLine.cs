using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class JournalEntryLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 JournalEntryLineId { get; set; }
        public Int64 JournalEntryId { get; set; }
        public int AccountId { get; set; }
        public DrOrCrSide DrCr { get; set; }
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public virtual JournalEntry JournalEntry { get; set; }
        public virtual Account Account { get; set; }
    }



}
