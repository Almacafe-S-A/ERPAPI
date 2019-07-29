using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class GeneralLedgerLine 
    {
        public GeneralLedgerLine()
        {
        }

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GeneralLedgerHeaderId { get; set; }
        public int AccountId { get; set; }
        public DrOrCrSide DrCr { get; set; }
        public decimal Amount { get; set; }
        public virtual Account Account { get; set; }
        public virtual GeneralLedgerHeader GeneralLedgerHeader { get; set; }
    }
}
