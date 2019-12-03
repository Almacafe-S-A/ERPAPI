using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace ERPAPI.Models
{
    public class ApplicationRole : IdentityRole<Guid>
    {
        // public string rolename { get; set; }

        public string Description { get; set; }

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
}
