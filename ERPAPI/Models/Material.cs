using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class Material
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 MaterialId { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        [Display(Name = "Codigo Material")]
        public string MaterialCode { get; set; }
        [Display(Name = "Tipo de Material")]
        public string MaterialType { get; set; }

        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }

        public List<MaterialDetail> MaterialDetail { get; set; } = new List<MaterialDetail>();
    }
}
