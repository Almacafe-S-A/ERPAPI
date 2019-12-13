using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class Substratum
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 SubstratumId { get; set; }
        [Required]
        public string SubstratumCode { get; set; }
        [Display(Name = "Descripcion")]
        public string Description { get; set; }

        public Int64 EstatusId { get; set; }

        public string Estatus { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }

        [Display(Name = "Usuario de creación")]
        public string UsuarioCreacion { get; set; }

        [Display(Name = "Usuario de modificación")]
        public string UsuarioModificacion { get; set; }
    }
}
