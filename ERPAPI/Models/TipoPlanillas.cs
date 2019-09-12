using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ERPAPI.Models
{
    public class TipoPlanillas
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdTipoPlanilla { get; set; }
        public string TipoPlanilla { get; set; }
        public string Descripcion { get; set; }
        public long EstadoId { get; set; }
        public string Estado { get; set; }


        public string Usuariocreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}