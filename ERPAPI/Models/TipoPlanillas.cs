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

        [Required]
        public string TipoPlanilla { get; set; }

        [Required]
        public string Descripcion { get; set; }

        [Required]
        public long EstadoId { get; set; }

        [ForeignKey("EstadoId")]
        public string Estado { get; set; }

        [Required]
        public string Usuariocreacion { get; set; }

        [Required]
        public string Usuariomodificacion { get; set; }
        
        public DateTime? FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }
    }
}