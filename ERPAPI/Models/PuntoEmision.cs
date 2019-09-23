using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class PuntoEmision
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 IdPuntoEmision { get; set; }

        [Display(Name = "Código")]
        public string PuntoEmisionCod { get; set; }

        [Display(Name = "Sucursal")]
        public Int64 BranchId { get; set; }
        [Display(Name = "Sucursal")]
        public string BranchName { get; set; }

        [Display(Name = "Estado")]
        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
