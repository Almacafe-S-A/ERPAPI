using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class PrecioCafe
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime Fecha { get; set; }

        public decimal PrecioBolsaUSD { get; set; }

        public string Cosecha { get; set; }

        public Int64? CustomerId { get; set; }

        public Customer Customer { get; set; }

        //public decimal DiferencialesUSD { get; set; }

        //public decimal TotalUSD { get; set; }

        public Int64 ExchangeRateId { get; set; }
        [ForeignKey("ExchangeRateId")]
        public ExchangeRate ExchangeRate { get; set; }

        public decimal? ExchangeRateValue { get; set; }
        [Column(TypeName = "Money")]
        public decimal? BrutoLPSIngreso { get; set; }

        public decimal PorcentajeIngreso { get; set; }
        [Column(TypeName = "Money")]
        public decimal? NetoLPSIngreso { get; set; }

        [Column(TypeName = "Money")]
        public decimal BrutoLPSConsumoInterno { get; set; }

        public decimal PorcentajeConsumoInterno { get; set; }
        [Column(TypeName = "Money")]
        public decimal NetoLPSConsumoInterno { get; set; }
        [Column(TypeName = "Money")]
        public decimal? TotalLPSIngreso { get; set; }

        public decimal BeneficiadoUSD { get; set; }

        public decimal FideicomisoUSD { get; set; }

        public decimal UtilidadUSD { get; set; }

        public decimal PermisoExportacionUSD { get; set; }
        [Column(TypeName = "Money")]
        public decimal TotalUSDEgreso { get; set; }
        [Column(TypeName = "Money")]
        public decimal? TotalLPSEgreso { get; set; }
        [Column(TypeName = "Money")]
        public decimal? PrecioQQOro { get; set; }
        [Column(TypeName = "Money")]
        public decimal? PercioQQPergamino { get; set; }
        [Column(TypeName = "Money")]
        public decimal? PrecioQQCalidadesInferiores { get; set; }

        public decimal? Otros { get; set; }

        public bool? UtilizadaCertificado { get; set; }


        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }






    }
}
