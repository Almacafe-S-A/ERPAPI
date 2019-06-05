using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Bitacora
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; } 
        public long? IdOperacion { get; set; } 
        public DateTime? HoraInicio { get; set; } 
        public DateTime? HoraFin { get; set; } 
        public string Accion { get; set; } 
        public string NoReferencia { get; set; } 
        public string Descripcion { get; set; }
        public string QueryEjecuto { get; set; }
        public string UsuarioEjecucion { get; set; }
        public string UsuarioModificacion { get; set; } 
        public string UsuarioCreacion { get; set; } 
        public DateTime? FechaModificacion { get; set; } 
        public DateTime? FechaCreacion { get; set; }
        public Int64 IdEstado { get; set; }
        public string Estado { get; set; } 
        public string ResultadoSerializado { get; set; }

    }
}
