using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CompanyInfo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 CompanyInfoId { get; set; }

        [Display(Name = "Empresa")]
        public string Company_Name{get;set;}

        [Display(Name = "Dirección")]
        public string Address { get; set; }

        [Display(Name = "Telefono")]
        public string Phone { get; set; }

        [Display(Name = "Fax")]
        public string Fax { get; set; }

        [Display(Name = "Correo")]
        [EmailAddress]
        public string Email { get; set; }

        [Display(Name = "RTN")]
        public string Tax_Id { get; set; }

        [Display(Name = "Imagen")]
        public string image { get; set; }

        [Display(Name = "Fecha de Creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de Modificación")]
        public DateTime FechaModificacion { get; set; }

        [Display(Name = "Usuario de Creación")]
        public string UsuarioCreacion { get; set; }

        [Display(Name = "Usuario de Modificación")]
        public string UsuarioModificacion { get; set; }

    }
}
