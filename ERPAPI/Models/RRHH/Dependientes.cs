using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class Dependientes
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdDependientes { get; set; }
        public string NombreDependientes { get; set; }
        public string Parentezco { get; set; }
        public string TelefonoDependientes { get; set; }
        public string DireccionDependientes { get; set; }
        public long? IdEmpleado { get; set; }

        public int? Edad { get; set; }
        public DateTime? FechaNacimiento { get; set; }

        public string Usuariocreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
