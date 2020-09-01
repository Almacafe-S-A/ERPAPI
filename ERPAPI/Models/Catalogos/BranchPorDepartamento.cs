using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class BranchPorDepartamento
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public long IdDepartamento { get; set; }
        public string NombreDepartamento { get; set; }

        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        
    }
}
