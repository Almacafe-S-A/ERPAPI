using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GeneralLedgerHeader
    {
        public DateTime Date { get; set; }
        public DocumentTypes DocumentType { get; set; }
        public string Description { get; set; }
    }



}
