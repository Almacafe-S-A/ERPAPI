using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ERPAPI.Models
{
    public class Planilla
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdPlanilla { get; set; }
        public string TipoPlanilla { get; set; }
        public string Descripcion { get; set; }
        public long EstadoId { get; set; }


        public string Usuariocreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}