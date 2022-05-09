using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class UnitOfMeasure
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int UnitOfMeasureId { get; set; }
        [Required]
        public string UnitOfMeasureName { get; set; }
        public string Description { get; set; }

        public Int64 IdEstado { get; set; }

        public string Estado { get; set; }

        
        public string UsuarioCreacion { get; set; }
        
        public string UsuarioModificacion { get; set; }
        
        public DateTime FechaCreacion { get; set; }
        
        public DateTime FechaModificacion { get; set; }
    }
}
