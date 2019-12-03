using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Kardex
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 KardexId { get; set; }

        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }
        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Fecha")]
        public DateTime KardexDate { get; set; }

        [Display(Name = "Tipo de documento")]
        public Int64 DocType { get; set; }
        [Display(Name = "Tipo de documento")]
        public string DocName { get; set; }

        [Display(Name = "Fecha de Kardex")]
        public DateTime DocumentDate { get; set; }

        [Display(Name = "Entrada/Salida")]
        public Int32 TypeOperationId { get; set; }

        [Display(Name = "Entrada/Salida")]
        public string TypeOperationName { get; set; }

        [Display(Name = "Moneda")]
        public Int64 CurrencyId { get; set; }

        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [Display(Name = "Moneda tasa")]
        public double Currency { get; set; }

        [Display(Name = "Documento")]
        public Int64 DocumentId { get; set; }

        [Display(Name = "Documento")]
        public string DocumentName { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }
        [Display(Name = "Usuario de creación")]
        public string UsuarioCreacion { get; set; }
        [Display(Name = "Usuario de modificación")]
        public string UsuarioModificacion { get; set; }

        public string Impreso { get; set; }

        public List<KardexLine> _KardexLine { get; set; } = new List<KardexLine>();
    }


    public class KardexDTO : Kardex
    {
       public List<Int64> Ids { get; set; } = new List<long>();
       public Int64 SalesOrderId { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Fecha de fin")]
        public DateTime? EndDate { get; set; }

    }


}
