using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Reconciliacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdReconciliacion { get; set; }
        public string DescripcionReconciliacion { get; set; }
        public DateTime? FechaReconciliacion { get; set; } 
        public long? IdCalculoplanilla { get; set; } 
        public DateTime? FechaAplicacion { get; set; }   
        public decimal? TotalReconciliacion { get; set; } 
        public decimal? SaldoEmpleado { get; set; } 
        public DateTime? FechaFinReconciliacion { get; set; } 
        public decimal? Tasacambio { get; set; } 
        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }
        public DateTime? Fechacreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string Usuariomodificacion { get; set; }

    }
}
