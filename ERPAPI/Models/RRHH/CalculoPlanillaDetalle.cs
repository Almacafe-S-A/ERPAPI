using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CalculoPlanillaDetalle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Iddetallecalculo { get; set; } // bigint
        public long? Idempleado { get; set; } // bigint
        public long? IdCalculo { get; set; } // bigint
        public long? IdPuesto { get; set; } // bigint
        public long? IdFormula { get; set; } // bigint
        public decimal? ValorFormula { get; set; } // numeric(18,4)
        public int? IdtipoFormula { get; set; } // integer
        public string NombreTipoFormula { get; set; } // text
        public string FormulaEjecutada { get; set; } // text
        public string Nombreempleado { get; set; } // text
        public string NombreFormula { get; set; } // text
        public long? IdOrdenFormula { get; set; } // bigint
        public DateTime? Fechacreacion { get; set; } // timestamp (6) without time zone
        public DateTime? FechaModificacion { get; set; } // timestamp (6) without time zone
        public string UsuarioCreacion { get; set; } // text
        public string UsuarioModificacion { get; set; } // text

    }
}
