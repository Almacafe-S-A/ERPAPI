using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ConciliacionLinea
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ConciliacionLineaId { get; set; }

        [ForeignKey("MotivoId")]
        [Display(Name = "Motivo Transacción")]
        public Int64? MotivoId { get; set; }

        [Display(Name = "Id Conciliacion")]
        public int ConciliacionId { get; set; }

        [Required]
        [Display(Name = "Crédito")]
        public double Credit { get; set; }

        [Required]
        [Display(Name = "Debito")]
        public double Debit { get; set; }

        [Required]
        [Display(Name ="Balance")]
        public Double Monto { get; set; }
        
        [Display(Name = "Referencia Bancaria")]
        public string ReferenciaBancaria { get; set; }
        
        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }

        [Display(Name = "Fecha Transaccion")]
        public DateTime TransDate { get; set; }

        [Display(Name = "Referencia Transacción")]
        public string ReferenceTrans { get; set; }

        [Display(Name = "Id de Asiento Contable")]
        public Int64? JournalEntryId { get; set; }
        [Display(Name = "Id de Línea de Asiento Contable")]
        public Int64? JournalEntryLineId { get; set; }

        [Display(Name = "Tipos de Voucher/Documento")]
        public Int64? VoucherTypeId { get; set; }

        [Display(Name = "Conciliado")]
        public bool Reconciled { get; set; }

        [Display(Name = "Numero de cheque")]
        public Int64? CheknumberId { get; set; }
        
        [Required]
        [Display(Name = "MonedaName")]
        public string MonedaName { get; set; }
        
        [Required]
        [Display(Name = "FechaCreacion")]
        public DateTime FechaCreacion { get; set; }
        
        [Display(Name = "FechaModificacion")]
        public DateTime FechaModificacion { get; set; }

        [Required]
        [Display(Name = "UsuarioCreacion")]
        public string UsuarioCreacion { get; set; }
        
        [Display(Name = "UsuarioModificacion")]
        public string UsuarioModificacion { get; set; }
    }
}
