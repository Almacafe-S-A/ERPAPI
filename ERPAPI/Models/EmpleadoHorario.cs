using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class EmpleadoHorario
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long EmpleadoId { get; set; }
        [Required]
        public long HorarioId { get; set; }

        [ForeignKey("EmpleadoId")]
        public Employees Empleado { get; set; }

        [ForeignKey("HorarioId")]
        public Horario HorarioEmpleado { get; set; }

        [Required]
        public long IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estados Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
