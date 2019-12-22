using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class QuotationDetail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 QuotationDetailId { get; set; }
        [ForeignKey("QuotationCode")]
        [Display(Name = "Codigo Cotizacion")]
        public Int64 QuotationCode { get; set; }
        [Display(Name = "Version")]
        public Int64 QuotationVersion { get; set; }
        [Display(Name = "Codigo detalle Cotizacion")]
        [ForeignKey("RecipeId")]
        public Int64 RecipeId { get; set; }
        [Display(Name = "Cantidad")]
        public double Quantity { get; set; }
        [ForeignKey("ProductId")]
        public Int64 ProductId { get; set; }
        [Display(Name = "Alto")]
        public double Height { get; set; }
        [Display(Name = "Ancho")]
        public double Width { get; set; }
        [Display(Name = "Grosor")]
        public double Thickness { get; set; }
        public double NumCara { get; set; }
        [Display(Name = "Adjunto")]
        public string Attachment { get; set; }
        [ForeignKey("MaterialId")]
        public Int64 MaterialId { get; set; }
        [Display(Name = "Tipo de Material")]
        public string MaterialType { get; set; }
        [ForeignKey("PaymentTypesId")]
        public Int64 PaymentTypesId { get; set; }
        [Display(Name = "Aplica Impuesto")]
        public bool ApplyTax { get; set; }
        [Display(Name = "Precio Unitario")]
        public double UnitPrice { get; set; }
        [Display(Name = "Porcentaje Anticipo")]
        public double AdvancePaymentPercent { get; set; }
        [Display(Name = "Vencimiento Cotizacion")]
        public DateTime QuotationDueDate { get; set; }
        [Display(Name = "Observaciones")]
        public string Observations { get; set; }
        [Display(Name = "Direccion Instalacion")]
        public string InstalationAddress { get; set; }
        [Display(Name = "Plazo de entrega")]
        public string DeliveryTerm { get; set; }
        [Display(Name = "Cantidad en letras")]
        public string QuantityInLetters { get; set; }
        [Display(Name = "Diseño")]
        public string Design { get; set; }
        [Display(Name = "Arreglo de Colores")]
        public string ColorArrangement { get; set; }
        [Display(Name = "Publicidad")]
        public string Publicity { get; set; }
        [Display(Name = "Texto")]
        public string Text { get; set; }
        [Display(Name = "Medida estructura")]
        public double StructuralMeasurement { get; set; }
        [Display(Name = "Forma de instalacion")]
        public string IntallationForm { get; set; }
        [Display(Name = "Tubo horizontal")]
        public string HorizontalTube { get; set; }
        [Display(Name = "Ubicacion")]
        public string Location { get; set; }
        [Display(Name = "Material caja")]
        public string BoxMaterial { get; set; }
        [Display(Name = "Observacion de instalacion")]
        public string InstallationObservation { get; set; }
        [Display(Name = "Tamaño letras")]
        public double LetterSize { get; set; }
        [Display(Name = "Relieve letras")]
        public string LetterRief { get; set; }
        [Display(Name = "Embozado letras")]
        public string LetterEmbossed { get; set; }
        [Display(Name = "Plano letras")]
        public string LetterFlat { get; set; }
        [Display(Name = "Base de concreto")]
        public string ConcreteBase { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
    }
}
