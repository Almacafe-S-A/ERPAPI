using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPAPI.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        [Display(Name = "Habilitado")]
        public bool? IsEnabled { get; set; }

        [Display(Name = "Sucursal")]
        public int BranchId { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        [ForeignKey("BranchId")]
        public  Branch Branch { get; set; }

        public virtual List<PasswordHistory> PasswordHistory { get; set; } = new List<PasswordHistory>();
    }

    public class ApplicationUserDTO : ApplicationUser
    {
        public bool cambiarpassword { get; set; }
    }


}
