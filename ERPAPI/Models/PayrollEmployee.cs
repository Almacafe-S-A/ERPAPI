using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class PayrollEmployee
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdPlanillaempleado { get; set; } 
        public long? IdPlanilla { get; set; }
        public long? IdEmpleado { get; set; } 
        public long? IdEstado { get; set; }
        public string Estado { get; set; } 
        public DateTime? FechaIngreso { get; set; } 
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; } 
        public string UsuarioCreacion { get; set; } 
        public string UsuarioModificacion { get; set; } 


    }
}
