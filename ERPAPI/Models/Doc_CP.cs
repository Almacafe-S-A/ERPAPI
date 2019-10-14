using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Doc_CP
    {
        
        public long Id { get; set; }
        
        public string DocNumber { get; set; }
        
        public int DocTypeId { get; set; }
        [ForeignKey("DocTipoId")]
        public ElementoConfiguracion DocType { get; set; }

        public DateTime DocDate { get; set; }

        public string  Description { get; set; }

        public double Amount { get; set; }

        public double PartialAmount { get; set; }

        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public DateTime DueDate { get; set; }

        public int PaymentQty { get; set; }

        public double Balance { get; set; }

        public double Balance_Mon { get; set; }

        public string  DocPaymentNumber { get; set; }

        public bool Payed { get; set; }  /// si/no

        public double LatePaymentAmount { get; set; }

        public double LatePaymentInterest { get; set; }
        

        public int DayTerms { get; set; }
        

        public string VendorDocumentId { get; set; }

        //public bool ImpuestoIncluido { get; set; }

        public string  Remarks { get; set; }

        public string AnnulationReason { get; set; }


        public Int64 TaxId { get; set; }

        [ForeignKey("TaxId")]
        public Tax Tax { get; set; }

        public long PaymentTypeId { get; set; }

        [ForeignKey("PaymentTypeId")]
        public PaymentTypes PaymentType { get; set; }


        public long AccountId { get; set; }

        [ForeignKey("AccountId")]
        public Accounting Account { get; set; }

        public bool Base { get; set; }

        public string PaymentNumber { get; set; }
        
        public string PaymentReference { get; set; }

        public int PaymentTerm { get; set; }
        
    }
}
