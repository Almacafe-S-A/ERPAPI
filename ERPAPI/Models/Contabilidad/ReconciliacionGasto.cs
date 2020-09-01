using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ReconciliacionGasto
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Idreconciliaciongasto { get; set; } // bigint
        public long? Idreconciliacion { get; set; } // bigint
        public string Descripciongasto { get; set; } // text
        public decimal? Montogasto { get; set; } // numeric(18,4)       
        public long? IdEmpleado { get; set; } // bigint
        public long? IdPlanilla { get; set; } // bigint
        public DateTime? Fechaaplicacion { get; set; } // timestamp (6) without time zone
        public DateTime? Fechacreacion { get; set; } // timestamp (6) without time zone
        public DateTime? Fechamodificacion { get; set; } // timestamp (6) without time zone
        public string Usuariocreacion { get; set; } // text
        public string Usuariomodificacion { get; set; } // text

    }
}
