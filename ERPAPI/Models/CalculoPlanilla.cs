using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CalculoPlanilla
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Idcalculo { get; set; } 
        public DateTime? Fechainicio { get; set; } 
        public DateTime? Fechafin { get; set; } 
        public string Descripcion { get; set; } 
        public long? IdPlanilla { get; set; }
        public decimal? TasaCambio { get; set; } 
        public Int64 IdEstado { get; set; }
        public string Estado { get; set; } 
        public DateTime? FechaCreacion { get; set; } 
        public DateTime? FechaModificacion { get; set; } 
        public string UsuarioCreacion { get; set; } 
        public string UsuarioModificacion { get; set; } 
     

    }
}
