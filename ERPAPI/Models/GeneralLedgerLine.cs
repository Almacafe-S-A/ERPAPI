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
        [Display(Name = "Id")]
        public Int64 GeneralLedgerHeaderId { get; set; }
        [Display(Name = "Id de Cuenta Contable")]
        public int AccountId { get; set; }
        [Display(Name = "Debito o Credito")]
        public DrOrCrSide DrCr { get; set; }
        [Display(Name = "Monto")]
        public decimal Amount { get; set; }
        public virtual Accounting Account { get; set; }
        public virtual GeneralLedgerHeader GeneralLedgerHeader { get; set; }
        [Required]
        [Display(Name = "Usuario de creacion")]
        public string UsuarioCreacion { get; set; }
        [Required]
        [Display(Name = "Usuario de modificacion")]
        public string UsuarioModificacion { get; set; }
        [Required]
        [Display(Name = "Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [Display(Name = "Fecha de modificacion")]
        public DateTime FechaModificacion { get; set; }

    }
}
