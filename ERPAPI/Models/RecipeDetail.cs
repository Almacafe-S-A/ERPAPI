using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class RecipeDetail
    {
        [Display(Name = "IngredientCode")]
        [Key]
        public Int64 IngredientCode { get; set; }
        [ForeignKey("RecipeId")]
        public Int64 RecipeId { get; set; }
        [ForeignKey("ProductId")]
        public Int64 ProductId { get; set; }
        [Display(Name = "Alto")]
        public double Height { get; set; }
        [Display(Name = "Ancho")]
        public double Width { get; set; }
        [Display(Name = "Grosor")]
        public double Thickness { get; set; }
        [Display(Name = "Cantidad")]
        public double Quantity { get; set; }
        public Int32 NumCara { get; set; }
        [Display(Name = "Adjunto")]
        public string Attachment { get; set; }
        [ForeignKey("MaterialId")]
        public Int64 MaterialId { get; set; }
        [Display(Name = "Tipo de Material")]
        public string MaterialType { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
