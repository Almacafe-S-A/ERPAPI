using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Conciliacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ConciliacionId { get; set; }

        [ForeignKey("IdBanco")]
        public Int64 BankId { get; set; }

        [ForeignKey("AccountId")]
        [Display(Name = "Id")]
        public Int64 AccountId { get; set; }


        [Required]
        [Display(Name = "BankName")]
        public string BankName { get; set; }
        [Display(Name = "Cuenta Bancaria")]
        public Int64 CheckAccountId { get; set; }

        [Required]
        [Display(Name = "FechaConciliacion")]
        public DateTime FechaConciliacion { get; set; }

        [Display(Name = "Fecha Inicio")]
        public DateTime DateBeginReconciled { get; set; }
        [Display(Name = "Fecha Fin")]
        public DateTime DateEndReconciled { get; set; }


        [Required]
        [Display(Name = "SaldoConciliado")]
        public Double SaldoConciliado { get; set; }

        
        [Required]
        [Display(Name = "FechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [Display(Name = "FechaModificacion")]
        public DateTime FechaModificacion { get; set; }

        [Required]
        [Display(Name = "UsuarioCreacion")]
        public string UsuarioCreacion { get; set; }

        [Required]
        [Display(Name = "UsuarioModificacion")]
        public string UsuarioModificacion { get; set; }

        [Required]
        [Display(Name="Saldo en Estado de Cuenta de Banco")]
        public decimal SaldoBanco { get; set; }

        [Required]
        [Display(Name="Saldo en Libro Mayor de Banco")]
        public decimal SaldoLibro { get; set; }

        public List<ConciliacionLinea> ConciliacionLinea { get; set; }

    }

    public class ConciliacionDTO : Conciliacion
    {
        public ConciliacionDTO(){}

        public ConciliacionDTO(Conciliacion a, string nombreCuenta)
        {
            ConciliacionId = a.ConciliacionId;
            BankId = a.BankId;
            AccountId = a.AccountId;
            BankName = a.BankName;
            CheckAccountId = a.CheckAccountId;
            FechaConciliacion = a.FechaConciliacion;
            DateBeginReconciled = a.DateBeginReconciled;
            DateEndReconciled = a.DateEndReconciled;
            SaldoConciliado = a.SaldoConciliado;
            FechaCreacion = a.FechaCreacion;
            FechaModificacion = a.FechaModificacion;
            UsuarioCreacion = a.UsuarioCreacion;
            UsuarioModificacion = a.UsuarioModificacion;
            SaldoBanco = a.SaldoBanco;
            SaldoLibro = a.SaldoLibro;
            NombreCuenta = nombreCuenta;
        }
        public string NombreCuenta { get; set; }
    }
}
