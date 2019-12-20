/********************************************************************************************************

 -- NAME   :  CRUDMeasure

 -- PROPOSE:  show Measure from company



 REVISIONS:



 version              Date                Author                        Description

 ----------           -------------       ---------------               -------------------------------
 1.0                  12/12/2019          Marvin.Guillen                Changes to create model
 

 ********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Measure
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 MeasurelId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Alto")]
        public decimal? High { get; set; }
        [Display(Name = "Ancho")]
        public decimal? width { get; set; }
        [Display(Name = "Grosor")]
        public decimal? thickness { get; set; }
        [Display(Name = "Cantidad")]
        public decimal? quantity { get; set; }
        [Display(Name = "Caras")]
        public decimal? faces { get; set; }
        [Display(Name = "Estado")]
        public long? IdEstado { get; set; }


        [Required]
        [Display(Name = "Usuario que lo crea")]
        public string CreatedUser { get; set; }

        [Required]
        [Display(Name = "Usuario que lo modifica")]
        public string ModifiedUser { get; set; }

        [Required]
        [Display(Name = "Fecha de creacion")]
        public DateTime CreatedDate { get; set; }

        [Required]
        [Display(Name = "Fecha de Modificacion")]
        public DateTime ModifiedDate { get; set; }

    }
}
