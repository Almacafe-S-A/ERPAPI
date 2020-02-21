using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class PhoneLines
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id Linea Telefonica")]
        public Int64 PhoneLineId { get; set; }

        [Display(Name = "Id de Empleado")]
        public long IdEmpleado { get; set; }
        [Display(Name = "Nombre de Empleado")]
        public string NombreEmpleado { get; set; }

        public int IdBranch { get; set; }
        [ForeignKey("IdBranch")]
        public Branch Branch { get; set; }

        [Display(Name = "Telefono de la empresa")]
        public string CompanyPhone { get; set; }

        [Display(Name = "Vencido Lps")]
        public double DueBalanceLps { get; set; }

        [Display(Name = "Vencido US")]
        public double DueBalanceUS { get; set; }

        [Display(Name = "Abono Lps")]
        public double PaymentLps { get; set; }

        [Display(Name = "Abono US")]
        public double PaymentUS { get; set; }

        [Display(Name = "Total Lps")]
        public double TotalLps { get; set; }

        [Display(Name = "Total US")]
        public double TotalUS { get; set; }

        public Int64 IdEstado { get; set; }

        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
