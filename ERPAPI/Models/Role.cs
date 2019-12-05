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


    public class RolesDTO
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public string NombreNormalizado { get; set; }
        public string ConcurrencyStamp { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string Descripcion { get; set; }
        public string Estado { get; set; }
        public int IdEstado { get; set; }
    }
}
