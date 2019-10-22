using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CustomerConditions
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CustomerConditionId { get; set; }
        public Int64 ConditionId { get; set; }
        public Int64 CustomerId { get; set; }
        public Int64 ProductId { get; set; }
        public Int64 SubProductId { get; set; }
        public Int64 DocumentId { get; set; }
        public Int64 IdTipoDocumento { get; set; }
        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }
        public string CustomerConditionName { get; set; }
        public string Description { get; set; }

        [Display(Name = "Condición logica")]
        public Int64 LogicalConditionId { get; set; }
        public string LogicalCondition { get; set; }
        public string ValueToEvaluate { get; set; }
        public double ValueDecimal { get; set; }
        public string ValueString { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
