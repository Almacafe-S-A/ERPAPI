using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.Models
{

    public class ApplicationUserRole : IdentityUserRole<Guid> 
    {
        public string UserName { get; set; }

        public string RoleName { get; set; }

        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }

        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public string UsuarioModificacion { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }
    }

    //public class  ApplicationUserRole<TKey> : IdentityUserRole<TKey> where TKey : IEquatable<TKey>
    //{

    //}

    public class UserRole 
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string rol { get; set; }
      

    }
}
