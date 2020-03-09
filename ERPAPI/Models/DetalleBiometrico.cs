using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class DetalleBiometrico
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long IdBiometrico { get; set; }

        [Required]
        public long IdEmpleado { get; set; }

        [Required]
        public DateTime FechaHora { get; set; }

        [Required]
        public string Tipo { get; set; }

        public Biometrico Encabezado { get; set; }

        [ForeignKey("IdEmpleado")]
        public Employees Empleado { get; set; }
    }
}
