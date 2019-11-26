using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GrupoEstado
    {
        [Key]
        [Display(Name = "Id")]
        public long Id { get; set; }
        [Display(Name = "Configuración")]
        public string Nombre { get; set; }
        [Display(Name = "Centro de costos")]
        public string Modulo { get; set; }
        public DateTime FechaCreacion { get; set; }
        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
