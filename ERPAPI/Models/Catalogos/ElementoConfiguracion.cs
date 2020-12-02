using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class ElementoConfiguracion
    {
        [Key]
        public long Id { get; set; } 
        public string Nombre { get; set; }
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Display(Name = "Estado")]
        public long IdEstado { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; } 
        
        public long? Idconfiguracion { get; set; } 
        public double? Valordecimal { get; set; } 
        public string Valorstring { get; set; } 
        public string Valorstring2 { get; set; } 
        public string Formula { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        [ForeignKey("Idconfiguracion")]
        public GrupoConfiguracion GrupoConfiguracion { get; set; }
    }


}
