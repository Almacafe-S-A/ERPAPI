using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class Departamento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; }
        public long? IdEstado { get; set; }

        public string Estado { get; set; }

        public bool? AplicaComision { get; set; }
        public long? ComisionId { get; set; }
        public long? PeridicidadId { get; set; }

        public string Usuariocreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
    }
}
