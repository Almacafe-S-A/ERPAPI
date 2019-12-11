using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InventoryTransfer
    {
        [Key]
        public int Id { get; set; }

        public string Description { get; set; }

        public int SourceBranchId { get; set; }
        [ForeignKey("SourceBranchId")]
        public Branch SourceBranch { get; set; }

        public int TargetBranchId { get; set; }
        [ForeignKey("TargetBranchId")]
        public Branch TargetBranch { get; set; }

        public DateTime DateGenerated { get; set; }

        public DateTime DepartureDate { get; set; }

        public DateTime DateReceived { get; set; }

        public long GeneratedbyEmployeeId { get; set; }
        [ForeignKey("GeneratedbyEmployeeId")]
        public Employees GeneratedbyEmployee { get; set; }

        public long ReceivedByEmployeeId { get; set; }
        [ForeignKey("ReceivedByEmployeeId")]
        public Employees ReceivedByEmployee { get; set; }

        public long CarriedByEmployeeId { get; set; }
        [ForeignKey("CarriedByEmployeeId")]
        public Employees CarriedEmployee { get; set; }

        public Int64 EstadoId { get; set; }
        [ForeignKey("EstadoId")]
        public Estados Estados { get; set; }

        public string Estado { get; set; }

        public int ReasonId { get; set; }

        public string Reason { get; set; }

        public string CAI { get; set; }

        public string NumeroSAR { get; set; }

        public string Rango { get; set; }

        public int TipoDocumentoId { get; set; }

        [ForeignKey("EstadoId")]
        public TiposDocumento TiposDocumento { get; set; }
        
        public long NumeracionSARId { get; set; }

        [ForeignKey("NumeracionSARId")]
        public NumeracionSAR NumeracionSAR { get; set; }





    }
}
