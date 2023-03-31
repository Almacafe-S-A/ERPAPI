using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ERPAPI.Models

{
    public class InvoicePayments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int NoPago { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public DateTime FechaPago { get; set; }        

        public decimal MontoAdeudaPrevio { get; set; }
        public decimal MontoPagado { get; set; }

        public decimal MontoAdeudado { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public string Depositante { get; set; }

        public string Observaciones { get; set; }

        


        public string Referrencia { get; set; }

        public Int64? CuentaBancariaId { get; set; }
        [ForeignKey("CuentaBancariaId ")]
        public AccountManagement accountManagement { get; set; }

        public Int64 Bank { get; set; }

        public string BankName { get; set; }

        public string CuentaBancaria { get; set; }

        public string NoDocumentos { get; set; }

        public int TipoPago { get; set; }

        public Int64 JournalId { get; set; }

        [ForeignKey("JournalId ") ]
        public JournalEntry journalEntry { get; set; }

        public string CantidadenLetras { get; set; }

        public int EstadoId { get; set; }

        public string Estado { get; set; }


        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public List<InvoicePaymentsLine> InvoicePaymentsLines { get; set; }







    }
}
