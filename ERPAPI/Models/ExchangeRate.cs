using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ExchangeRate
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 ExchangeRateId { get; set; }
        [Display(Name = "Dia de Tasa")]
        public DateTime DayofRate { get; set; }
        //[Display(Name = "Monto")]
        //public double ExchangeRateValue { get; set; }
        [Display(Name = "Monto Decimal")]
        [Column(TypeName = "decimal(18,4)")]
        public decimal ExchangeRateValue { get; set; }

        public Currency Currency { get; set; }

        [Display(Name = "Moneda")]
        [ForeignKey("CurrencyId")]
        public int CurrencyId { get; set; }

        [Display(Name = "Moneda")]
       
        public string CurrencyName { get; set; }

        [Required]
        [Display(Name = "Usuario que lo crea")]
        public string CreatedUser { get; set; }

        [Required]
        [Display(Name = "Usuario que lo modifica")]
        public string ModifiedUser { get; set; }

        [Required]
        [Display(Name = "Fecha de creacion")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Fecha de Modificacion")]
        public DateTime ModifiedDate { get; set; }
    }
}
