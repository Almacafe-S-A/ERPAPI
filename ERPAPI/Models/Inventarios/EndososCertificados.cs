using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class EndososCertificados
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 EndososCertificadosId { get; set; }

        [Display(Name = "Id certificado")]
        public Int64 IdCD { get; set; }

        [Display(Name = "Número de certificado")]
        public Int64 NoCD { get; set; }

        [Display(Name = "Fecha de documento")]
        public DateTime DocumentDate { get; set; }
        

        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }

        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Producto")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Producto")]
        public string ProductName { get; set; }

        [Display(Name = "Banco")]
        public Int64 BankId { get; set; }

        [Display(Name = "Banco")]
        public string BankName { get; set; }

        [Display(Name = "Cantidad a endosar")]
        public decimal CantidadEndosar { get; set; }

        [Display(Name = "Moneda")]
        public Int64 CurrencyId { get; set; }

        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [Display(Name = "Valor a endosar")]
        public decimal ValorEndosar { get; set; }

        [Display(Name = "Cantidad a endosar")]
        public string FirmadoEn { get; set; }

        public string ProductoEndosado { get; set; }


        [Display(Name = "Tipo de endoso")]
        public Int64 TipoEndoso { get; set; }

        [Display(Name = "Nombre de endoso")]
        public string NombreEndoso { get; set; }

        [Display(Name = "Fecha de vencimiento")]
        public DateTime ExpirationDate { get; set; }

        [Display(Name = "Fecha otorgada")]
        public DateTime FechaOtorgado { get; set; }

        [Display(Name = "Tasa de interes")]
        public decimal TasaDeInteres { get; set; }

        [Display(Name = "Total endoso")]
        public decimal TotalEndoso { get; set; }

        public string Estado { get; set; }

        public int EstadoId { get; set; }


        public DateTime? FechaLiberacion { get; set; }/// automatica al cancelar un endoso

        public DateTime? FechaCancelacion { get; set; }/// solo sera editable en las liberaciones


        public decimal Saldo { get; set; } /// solo se va mostrar en el grid Princial de endosos para referencias 


        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public string Impreso { get; set; }
        public List<EndososCertificadosLine> EndososCertificadosLine { get; set; } = new List<EndososCertificadosLine>();

    }


    public class EndososDTO : EndososCertificados
    {
        public List<Int64> TipoEndosoIdList { get; set; }
        public Int64 editar { get; set; } = 1;
    }


}
