using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GarantiaBancaria
    {
        public int Id { get; set; }

        public string strign { get; set; }

        public DateTime FechaInicioVigencia { get; set; }

        public DateTime FechaFianlVigencia { get; set; }

        public string NumeroCertificado { get; set; }
        public Int64 CostCenterId { get; set; }
        [ForeignKey("CostCenterId")]
        public CostCenter CostCenter { get; set; }

        public double Monto { get; set; }

        public int CurrencyId { get; set; }
        [ForeignKey("CurrencyId")]
        public Currency Currency { get; set; }

        public double Ajuste { get; set; }

        public Int64 IdEstado { get; set; }
        public Estados Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }






    }
}
