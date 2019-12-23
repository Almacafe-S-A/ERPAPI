using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class MotivoConciliacion
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public long MotivoId { get; set; }

        [Required]
        [Display(Name="Nombre")]
        public string Nombre { get; set; }
    }
}
