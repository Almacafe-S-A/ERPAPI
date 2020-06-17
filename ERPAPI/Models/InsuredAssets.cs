using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InsuredAssets
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Int64 InsurancePolicyId { get; set; }

        [ForeignKey("InsurancePolicyId")]
        public InsurancePolicy InsurancePolicy { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch   Branch { get; set; }

        public int? WarehouseId { get; set; }
        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        public string AssetName { get; set; }

        public decimal AssetDeductible { get; set; }

        public decimal AssetInsuredValue { get; set; }

        public decimal MerchadiseTotalValue { get; set; }

        public decimal MerchandiseDeductible { get; set; }

        public decimal MerchandiseInsuredValue { get; set; }

        public decimal InsuredDiference { get; set; }

        public long CategoryId { get; set; }
        [ForeignKey("CategoryId")]

        public ElementoConfiguracion Category { get; set; }

        public Int64 EstadoId { get; set; }
        [ForeignKey("EstadoId")]
        public Estados Estados { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

    }
}
