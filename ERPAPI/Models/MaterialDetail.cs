using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class MaterialDetail
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 MaterialDetailId { get; set; }
        [ForeignKey("MaterialId")]
        public Int64 MaterialId { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }
        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
