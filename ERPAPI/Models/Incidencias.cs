using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Incidencias
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdIncidencia { get; set; } 
        public DateTime? FechaInicio { get; set; } 
        public DateTime? FechaFin { get; set; } 
        public long? IdEmpleado { get; set; } 
        public string DescripcionIncidencia { get; set; }
        public long? IdTipoIncidencia { get; set; }

        public DateTime? FechaCreacion { get; set; } 
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; } 
        public string UsuarioModificacion { get; set; }
       
    }
}
