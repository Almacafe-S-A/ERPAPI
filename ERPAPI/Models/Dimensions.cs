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
        public string Description { get; set; }

    }
}
