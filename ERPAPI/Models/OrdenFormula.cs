using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class OrdenFormula
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Idordenformula { get; set; } // bigint
        public long? IdPlanilla { get; set; } // bigint
        public long? Idformula { get; set; } // bigint
        public int? Orden { get; set; } // integer
        public Int64 IdEstado { get; set; }
        public string Estado { get; set; } // text
        public DateTime? FechaCreacion { get; set; } // timestamp (6) without time zone
        public DateTime? FechaModificacion { get; set; } // timestamp (6) without time zone
        public string UsuarioCreacion { get; set; } // text
        public string UsuarioModificacion { get; set; } // text

    }
}
