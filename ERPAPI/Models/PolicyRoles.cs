using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class PolicyRoles
    {
        
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        public Guid IdPolicy { get; set; }
        [ForeignKey("IdPolicy")]
        public Policy Policy { get; set; }

        public string PolicyName { get; set; }

        [Required]
        public Guid IdRol { get; set; }
        [ForeignKey("IdRol")]
        public ApplicationRole Role { get; set; }

        public string RolName { get; set; }

        [Required]
        public string UsuarioCreacion { get; set; }

        [Required]
        public string UsuarioModificacion { get; set; }

        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }

    }


}
