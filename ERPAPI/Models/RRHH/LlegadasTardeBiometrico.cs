using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class LlegadasTardeBiometrico
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long IdBiometrico { get; set; }

        [Required]
        public long IdEmpleado { get; set; }

        [Required]
        public int Horas { get; set; }

        [Required]
        public int Minutos { get; set; }

        [Display(Name = "Fecha")]
        public DateTime Fecha { get; set; }

        [Display(Name = "Dia")]
        public int Dia { get; set; }
        public Biometrico Encabezado { get; set; }

        [ForeignKey("IdEmpleado")]
        public Employees Empleado { get; set; }

        [Required]
        public long IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estados Estado { get; set; }

        public int ControlAsistenciaId { get; set; }
        [ForeignKey("Id")]
        public ControlAsistencias ControlAsistencias { get; set; }
    }
}
