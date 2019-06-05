using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class HoursWorkedDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdDetallehorastrabajadas { get; set; } // bigint
        public long? IdHorasTrabajadas { get; set; } // bigint
        public DateTime? Horaentrada { get; set; } // timestamp (6) without time zone
        public DateTime? Horasalida { get; set; } // timestamp (6) without time zone
        public decimal? Multiplicahoras { get; set; } // numeric(18,4)
        public string UsuarioCreacion { get; set; } // text
        public string UsuarioModificacion { get; set; } // text
        public DateTime? FechaCreacion { get; set; } // timestamp (6) without time zone
        public DateTime? FechaModificacion { get; set; } // timestamp (6) without time zone

        #region Associations


        // public HorasTrabajadas IdHorasTrabajadasconstrain { get; set; }

        #endregion

    }
}
