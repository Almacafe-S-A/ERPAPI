using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Biometrico
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public DateTime Fecha { get; set; }

        [Required]
        public string Observacion { get; set; }

        [Required]
        public long IdEstado { get; set; }

        [ForeignKey("IdBiometrico")]
        public List<DetalleBiometrico> Detalle { get; set; }

        [ForeignKey("IdEstado")]
        public Estados Estado { get; set; }
    }
}
