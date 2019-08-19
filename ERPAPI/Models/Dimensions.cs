using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Dimensions
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Numero de Centro de Costo")]
        [Required]
        [StringLength(30)]
        public string Num { get; set; }
        [Required]
        [Display(Name = "Codigo de Centro")]
        public SysDimension DimCode { get; set; }

        [StringLength(60)]
        [Display(Name = "Nombre de Centro Costo")]
        public string Description { get; set; }
        [Display(Name = "Id de estado")]
        public Int64 IdEstado { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Required]
        [Display(Name = "Usuario de creacion")]
        public string UsuarioCreacion { get; set; }
        [Required]
        [Display(Name = "Usuario de modificacion")]
        public string UsuarioModificacion { get; set; }
        [Required]
        [Display(Name = "Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [Display(Name = "Fecha de Modificacion")]
        public DateTime FechaModificacion { get; set; }

    }
}
