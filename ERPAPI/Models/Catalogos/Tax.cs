using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]  
    [Route("api/Tax")]
    [ApiController]
    public class Tax
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 TaxId { get; set; }
        public string TaxCode { get; set; }
        public string Description { get; set; }
        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }
        public decimal TaxPercentage { get; set; }

        public Int64? CuentaContableIngresosId { get; set; }
        public string CuentaContableIngresosNombre { get; set; }
        [ForeignKey("CuentaImpuestoporCobrarId")]
        public Accounting CuentaContableIngresos { get; set; }
        public Int64? CuentaContablePorCobrarId { get; set; }
        public string CuentaContablePorCobrarNombre { get; set; }
        [ForeignKey("CuentaImpuestoporPagarId")]
        public Accounting CuentaContablePorCobrar { get; set; }

        public Int64? CuentaImpuestoPagadoId { get; set; }
        public string CuentaImpuestoPagadoNombre { get; set; }
        [ForeignKey("CuentaImpuestoPagadoId")]
        public Accounting CuentaImpuestoPagado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }


}
