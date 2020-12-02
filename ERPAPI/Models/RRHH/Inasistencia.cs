using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Inasistencia
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public long IdEmpleado { get; set; }

        public string Observacion { get; set; }

        [Required]
        public long TipoInasistencia { get; set; }

        [ForeignKey("IdEmpleado")]
        public Employees Empleado { get; set; }

        [Required]
        public long IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estados Estado { get; set; }

        [ForeignKey("TipoInasistencia")]
        public ElementoConfiguracion Tipo { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
