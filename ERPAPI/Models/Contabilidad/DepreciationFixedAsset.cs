using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class DepreciationFixedAsset
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 DepreciationFixedAssetId { get; set; }

        [Display(Name = "Id")]
        public Int64 FixedAssetId { get; set; }

        [Display(Name = "Año")]
        public Int32 Year { get; set; }
        [Column(TypeName = "Money")]

        [Display(Name = "Enero")]
        public decimal January { get; set; }
        [Column(TypeName = "Money")]

        [Display(Name = "Febrero")]
        public decimal February { get; set; }

        [Display(Name = "Marzo")]
        [Column(TypeName = "Money")]
        public decimal March { get; set; }

        [Display(Name = "Abril")]
        [Column(TypeName = "Money")]
        public decimal April { get; set; }

        [Display(Name = "Mayo")]
        [Column(TypeName = "Money")]
        public decimal May { get; set; }

        [Display(Name = "Junio")]
        [Column(TypeName = "Money")]
        public decimal June { get; set; }

        [Display(Name = "Julio")]
        [Column(TypeName = "Money")]
        public decimal July { get; set; }

        [Display(Name = "Agosto")]
        [Column(TypeName = "Money")]
        public decimal August { get; set; }

        [Display(Name = "Septiembre")]
        [Column(TypeName = "Money")]
        public decimal September { get; set; }

        [Display(Name = "Octubre")]
        [Column(TypeName = "Money")]
        public decimal October { get; set; }

        [Display(Name = "Noviembre")]
        [Column(TypeName = "Money")]
        public decimal November { get; set; }

        [Display(Name = "Diciembre")]
        [Column(TypeName = "Money")]
        public decimal December { get; set; }

        [Display(Name = "Depreciado")]
        [Column(TypeName = "Money")]
        public decimal TotalDepreciated { get; set; }


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
