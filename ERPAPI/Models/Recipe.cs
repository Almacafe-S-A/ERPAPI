using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class Recipe
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 RecipeId { get; set; }
        [Display(Name = "Nombre de la receta")]
        public string RecipeCode { get; set; }
        [Display(Name = "Descripcion")]
        public string Description { get; set; }
        public Int64 EstatusId { get; set; }

        public string Estatus { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
