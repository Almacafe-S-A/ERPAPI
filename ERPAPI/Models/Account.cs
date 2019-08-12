using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Account
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 AccountId { get; set; }

        public int? ParentAccountId { get; set; }

        public Int64 CompanyInfoId { get; set; }
        [Required]
        [StringLength(50)]
        public string AccountCode { get; set; }
        [Required]
        [StringLength(200)]
        public string AccountName { get; set; }
        [StringLength(200)]
        public string Description { get; set; }
        public bool IsCash { get; set; }
        public bool IsContraAccount { get; set; }
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
        public DateTime FechaModificacion { get; set; }

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        public byte[] RowVersion { get; set; }
        public virtual Account ParentAccount { get; set; }

        public virtual AccountClass AccountClass { get; set; }

        public virtual CompanyInfo Company { get; set; }

        public virtual ICollection<Account> ChildAccounts { get; set; }

        [NotMapped]
        public virtual ICollection<MainContraAccount> ContraAccounts { get; set; }
        public virtual ICollection<GeneralLedgerLine> GeneralLedgerLines { get; set; }


        private decimal GetDebitCreditBalance(DrOrCrSide side)
        {
            decimal balance = 0;

            if (side == DrOrCrSide.Dr)
            {
                var dr = from d in GeneralLedgerLines
                         where d.DrCr == DrOrCrSide.Dr
                         select d;

                balance = dr.Sum(d => d.Amount);
            }
            else
            {
                var cr = from d in GeneralLedgerLines
                         where d.DrCr == DrOrCrSide.Cr
                         select d;

                balance = cr.Sum(d => d.Amount);
            }

            return balance;
        }

        public decimal GetBalance()
        {
            decimal balance = 0;

            var dr = from d in GeneralLedgerLines
                     where d.DrCr == DrOrCrSide.Dr
                     select d;

            var cr = from c in GeneralLedgerLines
                     where c.DrCr == DrOrCrSide.Cr
                     select c;

            decimal drAmount = dr.Sum(d => d.Amount);
            decimal crAmount = cr.Sum(c => c.Amount);

            if (AccountClass.NormalBalance == "Dr")
            {
                balance = drAmount - crAmount;
            }
            else
            {
                balance = crAmount - drAmount;
            }

            return balance;
        }

        public bool CanPost()
        {
            if (ChildAccounts != null && ChildAccounts.Count > 0)
                return false;
            return true;
        }

        /// <summary>
        /// Used to indicate the increase or decrease on account. When there is a change in an account, that change is indicated by either debiting or crediting that account.
        /// </summary>
        /// <param name="amount">The amount to enter on account.</param>
        /// <returns></returns>
        public DrOrCrSide DebitOrCredit(decimal amount)
        {
            var side = DrOrCrSide.Dr;

            if (this.AccountId == (int)AccountClasses.Assets || this.AccountId == (int)AccountClasses.Expense)
            {
                if (amount > 0)
                    side = DrOrCrSide.Dr;
                else
                    side = DrOrCrSide.Cr;
            }

            if (this.AccountId == (int)AccountClasses.Liabilities || this.AccountId == (int)AccountClasses.Equity || this.AccountId == (int)AccountClasses.Revenue)
            {
                if (amount < 0)
                    side = DrOrCrSide.Dr;
                else
                    side = DrOrCrSide.Cr;
            }

            if (this.IsContraAccount)
            {
                if (side == DrOrCrSide.Dr)
                    return DrOrCrSide.Cr;
                if (side == DrOrCrSide.Cr)
                    return DrOrCrSide.Dr;
            }

            return side;
        }

    }
}
