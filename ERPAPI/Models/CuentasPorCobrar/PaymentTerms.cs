using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class PaymentTerms
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }

        public Int64 PaymentTypesId { get; set; }

        [ForeignKey("PaymentTypesId")]
        public PaymentTypes PaymentTypes { get; set; }

        public double Amount { get; set; }

        public int Days { get; set; }

        public int Fees { get; set; }

        public double FirstPayment { get; set; }

        public double EarlyPaymentDiscount { get; set; }

        public Accounting Accounting { get; set; }

        public string ChekingAccount { get; set; }

        public int Default { get; set; }

        public string CustomerPayIn { get; set; }

        [Required]
        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }
        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }


    }
}
