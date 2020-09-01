using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ReconciliacionEscala
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdEscalareconciliacion { get; set; } // bigint
        public long? IdEmpleado { get; set; } // bigint
        public long? IdEscala { get; set; } // bigint
        public string DescripcionEscala { get; set; } // text
        public long? IdConcepto { get; set; } // bigint
        public string NombreConcepto { get; set; } // text
        public decimal? MontoEscala { get; set; } // numeric(18,4)
        public long? IdReconciliacion { get; set; } // bigint     
        public long? IdPlanilla { get; set; } // bigint
        public decimal? Montoretenido { get; set; } // numeric(18,4)
        public decimal? DiferenciaPorretener { get; set; } // numeric(18,4)
        public int? MesesRestantes { get; set; } // integer
        public int? MesesEjecutados { get; set; } // integer
        public decimal? Montotrecesalario { get; set; } // numeric(18,4)
        public decimal? Montocatorcesalario { get; set; } // numeric(18,4)
        public decimal? Montoquincesalario { get; set; } // numeric(18,4)
        public DateTime? FechaCreacion { get; set; } // timestamp (6) without time zone
        public DateTime? FechaModificacion { get; set; } // timestamp (6) without time zone
        public string UsuarioCreacion { get; set; } // text
        public string UsuarioModificacion { get; set; } // text

    }
}
