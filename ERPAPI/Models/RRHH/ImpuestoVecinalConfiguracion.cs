using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ImpuestoVecinalConfiguracion
    {
        [Required]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public decimal De { get; set; }

        [Required]
        public decimal Hasta { get; set; }

        [Required]
        public decimal FactorMillar { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        [Required]
        public DateTime FechaModificacion { get; set; }

        [Required]
        public string UsuarioModificacion { get; set; }

        [Required]
        public string UsuarioCreacion { get; set; }
    }
}
