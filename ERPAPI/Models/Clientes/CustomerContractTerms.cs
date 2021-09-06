using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CustomerContractTerms
    {
        public int Id { get; set; }

        public string Term { get; set; }

        
        public Int64 TypeInvoiceId { get; set; }
        [ForeignKey("TypeInvoiceId")]

        public string TypeInvoiceName { get; set; }

        public int Pro { get; set; }

    }
}
