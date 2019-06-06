using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Currency
    {
        public int CurrencyId { get; set; }
        [Required]
        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }
        [Required]
        public string CurrencyCode { get; set; }
        public string Description { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
    }
}
