using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class PurchPartners
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 PartnerId { get; set; }
        [Display(Name = "Nombre")]
        public string PartnerName { get; set; }

        [Display(Name = "IdProveedor")]
        public Int64 PurchId { get; set; }



        [Display(Name = "Identidad")]
        public string Identidad { get; set; }
        public string RTN { get; set; }

        [Display(Name = "Telefono")]
        public string Telefono { get; set; }

        [Display(Name = "Listados sancionados")]
        public string Listados { get; set; }

        [EmailAddress]
        public string Correo { get; set; }
        [Display(Name = "Usuario de creacion")]
        public string CreatedUser { get; set; }
        [Display(Name = "Usuario de Modificacion")]
        public string ModifiedUser { get; set; }
        [Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }
        [Display(Name = "Fecha de Modificación")]
        public DateTime ModifiedDate { get; set; }
    }
}
