/********************************************************************************************************

 -- NAME   :  CRUDInstallmentDelivery

 -- PROPOSE:  show InstallmentDelivery from company



 REVISIONS:



 version              Date                Author                        Description

 ----------           -------------       ---------------               -------------------------------
 1.0                  13/12/2019          Marvin.Guillen                Changes to create model
 

 ********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InstallmentDelivery
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 InstallmentDeliveryId { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
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
