using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Accounting
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 AccountId { get; set; }

        [Display(Name = "Id Jerarquia Contable")]
        public int? ParentAccountId { get; set; }
        [Display(Name = "Id de la Empresa")]
        public Int64 CompanyInfoId { get; set; }

        [Display(Name = "Saldo Contable")]
        public double AccountBalance { get; set; }

        [MaxLength(5000)]
        [Display(Name = "Descripcion de la cuenta")]
        public string Description { get; set; }
        [Display(Name = "Mostar Saldos")]
        public bool IsCash { get; set; }
        public Int64 TypeAccountId { get; set; }
        [Display(Name = "Bloqueo para Diarios:")]
        public bool BlockedInJournal { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Codigo Contable")]
        public string AccountCode { get; set; }
        [Display(Name = "Id de estado")]
        public Int64? IdEstado { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "Nivel de Jerarquia:")]
        public Int64 HierarchyAccount { get; set; }
        [Required]
        [StringLength(200)]
        [Display(Name = "Nombre de la cuenta")]
        public string AccountName { get; set; }

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

        [Column(TypeName = "timestamp")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        [MaxLength(8)]
        [Display(Name = "Version Fila")]
        public byte[] RowVersion { get; set; }
        [Display(Name = "Padre de la cuenta")]
        public virtual Accounting ParentAccount { get; set; }

        [Display(Name = "Cuenta Totaliza")]
        public bool Totaliza { get; set; }

        [Display(Name = "Cuenta Deudora / Acreedora")]
        public string DeudoraAcreedora { get; set; }

        public virtual CompanyInfo Company { get; set; }

        public virtual List<Accounting> ChildAccounts { get; set; }
        
    }


    public class AccountingDTO : Accounting
    {
        public AccountingDTO()
        {
            
        }
    
        public double Debit { get; set; }

        public double Credit { get; set; }

        public double TotalDebit { get; set; }

        public double TotalCredit { get; set; }

        public bool? estadocuenta { get; set; }

        public List<AccountingDTO> Children { get; set; } = new List<AccountingDTO>();
    }

    public class AccountingFilter
    {
        public Int64 TypeAccountId { get; set; }
        public bool? estadocuenta { get; set; }

    }


}
