using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CustomerRisk
    {
        public int Id { get; set; }

        public Int64 CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public decimal ValorSeveridad { get; set; }

        public string NivelSeveridad { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }

        [Display(Name = "Usuario de creación")]
        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }
    }
}
