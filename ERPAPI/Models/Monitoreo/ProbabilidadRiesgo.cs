using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ProbabilidadRiesgo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public Int64 Nivel { get; set; }
        public string Descriptor { get; set; }
        public string Descripcion { get; set; }
        public string Frecuencia { get; set; }
        public string ColorHexadecimal { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
