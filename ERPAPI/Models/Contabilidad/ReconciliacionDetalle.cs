using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ReconciliacionDetalle
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdDetallereconciliacion { get; set; } // bigint
        public long? IdReconciliacion { get; set; } // bigint
        public long? IdEmpleado { get; set; } // bigint
        public int? Year { get; set; } // integer
        public int? Month { get; set; } // integer
        public int? Dia { get; set; } // integer
        public string Descripcion { get; set; }
        public decimal? Salario { get; set; }
        public decimal? Horasextras { get; set; }
        public decimal? Bonos { get; set; } // numeric(18,4)
        public decimal? OtrosIngresos { get; set; } // numeric(18,4)
        public decimal? SalarioRecibido { get; set; } // numeric(18,4)
        public long? IdPlanilla { get; set; } // bigint
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } // text
        public string UsuarioModificacion { get; set; } // text
        public decimal? Deducciones { get; set; } // numeric(18,4)
        public decimal? CatorceSalarioProporcional { get; set; } // numeric(18,4)
        public decimal? TrecesalarioProporcional { get; set; } // numeric(18,4)
        public decimal? Quincesalarioproporcional { get; set; } // numeric(18,4)
    }
}
