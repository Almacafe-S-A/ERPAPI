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

        [Display(Name = "Enero")]
        public decimal January { get; set; }

        [Display(Name = "Febrero")]
        public decimal February { get; set; }

        [Display(Name = "Marzo")]
        public decimal March { get; set; }

        [Display(Name = "Abril")]
        public decimal April { get; set; }

        [Display(Name = "Mayo")]
        public decimal May { get; set; }

        [Display(Name = "Junio")]
        public decimal June { get; set; }

        [Display(Name = "Julio")]
        public decimal July { get; set; }

        [Display(Name = "Agosto")]
        public decimal August { get; set; }

        [Display(Name = "Septiembre")]
        public decimal September { get; set; }

        [Display(Name = "Octubre")]
        public decimal October { get; set; }

        [Display(Name = "Noviembre")]
        public decimal November { get; set; }

        [Display(Name = "Diciembre")]
        public decimal December { get; set; }

        [Display(Name = "Depreciado")]
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
