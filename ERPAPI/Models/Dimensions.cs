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
        [Display(Name = "Dimension Numero")]
        [Required]
        [StringLength(30)]
        public string Num { get; set; }
        [Required]
        public SysDimension DimCode { get; set; }
        [StringLength(60)]
        [Display(Name = "Nombre ")]
        public string Description { get; set; }

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
        public DateTime FechaModificacion { get; set; }

    }
}
