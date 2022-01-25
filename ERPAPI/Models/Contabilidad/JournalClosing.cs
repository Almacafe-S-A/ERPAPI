using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class JournalClosing
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public Int64 JournalEntryId { get; set; }
        [ForeignKey("JournalEntryId")]
        public JournalEntry JournalEntry { get; set; }
        public int YearClosed { get; set; }

    }
}
