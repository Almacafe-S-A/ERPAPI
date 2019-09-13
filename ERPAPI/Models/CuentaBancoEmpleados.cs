using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CuentaBancoEmpleados
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public long BankId { get; set; }
        public string BankName { get; set; }
        public string NumeroCuenta { get; set; }
        public long IdEmpleado { get; set; }
        public string NombreEmpleado { get; set; }

        public string Usuariocreacion { get; set; }
        public string Usuariomodificacion { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }

    }
}
