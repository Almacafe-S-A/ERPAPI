using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class FixedAsset
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 FixedAssetId { get; set; }

        [Display(Name = "Nombre de activo fijo")]
        public string FixedAssetName { get; set; }

        [Display(Name = "Ubicacion")]
        public string Ubicacion { get; set; }

        [Display(Name = "Fecha del activo")]
        public DateTime AssetDate { get; set; }


        [Display(Name = "Grupo del activo fijo")]
        public Int64 FixedAssetGroupId { get; set; }
        [ForeignKey("FixedAssetGroupId")]
        public FixedAssetGroup FixedAssetGroup { get; set; }

        [Display(Name = "Código de grupo")]
        public string FixedAssetCode { get; set; }

        [Display(Name = "Sucursal")]
        public Int64 BranchId { get; set; }

        [Display(Name = "Sucursal")]
        public string BranchName { get; set; }


        [Display(Name = "Estado")]
        public Int64 IdEstado { get; set; }

        [Display(Name = "Estado")]
        public string Estado { get; set; }


        [Display(Name = "Bodega")]
        public Int64 WareHouseId { get; set; }

        [Display(Name = "Bodega")]
        public string WareHouseName { get; set; }

        [Display(Name = "Centro de costos")]
        public Int64 CenterCostId { get; set; }

        [Display(Name = "Centro de costos")]
        public string CenterCostName { get; set; }


        [Display(Name = "Vida útil")]
        public decimal FixedActiveLife { get; set; }


        [Display(Name = "Monto")]
        [Column(TypeName = "Money")]
        public decimal Amount { get; set; }

        [Display(Name = "Costo")]
        [Column(TypeName = "Money")]
        public decimal? Cost { get; set; }

        [Display(Name = "Valor residual")]
        [Column(TypeName = "Money")]
        public decimal ResidualValue { get; set; }

        [Display(Name = "A depreciar")]
        [Column(TypeName = "Money")]
        public decimal ToDepreciate { get; set; }

        [Display(Name = "Total Depreciado")]
        [Column(TypeName = "Money")]
        public decimal TotalDepreciated { get; set; }

        [Display(Name = "Costo total del activo")]
        [Column(TypeName = "Money")]
        public decimal ActiveTotalCost { get; set; }

        [Display(Name = "Valor residual porcentaje")]
        public decimal ResidualValuePercent { get; set; }

        [Display(Name = "Valor neto")]
        [Column(TypeName = "Money")]
        public decimal NetValue { get; set; }

        public string Codigo { get; set; }

        public string Marca { get; set; }

        public string Serie { get; set; }

        public string Modelo { get; set; }

        public decimal VidaUtilNIIF { get; set; }

        public decimal DepreciacionMensualNIIF { get; set; }

        public decimal TotalaDepreciarNIIF { get; set; }

        [Display(Name = "Depreciacion acumulada")]
        [Column(TypeName = "Money")]
        public decimal AccumulatedDepreciation { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }

        [Display(Name = "Usuario de creación")]
        public string UsuarioCreacion { get; set; }

        [Display(Name = "Usuario de modificación")]
        public string UsuarioModificacion { get; set; }
    }


    public class FixedAssetDTO : FixedAsset
    {
        public List<FixedAsset> _FixedAsset { get; set; }
        public int editar { get; set; } = 1;
        public string token { get; set; }

        public int MotivoId { get; set; }

        public DateTime FechaBaja { get; set; }
    }


}
