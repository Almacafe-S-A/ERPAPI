using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
     public class ApplicationUser : IdentityUser<Guid>
   // public class ApplicationUser : IdentityUser<string, IdentityUserLogin, IdentityUserRole, ApplicationUserClaim>
    {
        public Int64 BranchId { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
