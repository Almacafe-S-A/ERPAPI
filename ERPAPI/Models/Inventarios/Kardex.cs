using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Kardex
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 KardexId { get; set; }

        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }
        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Fecha")]
        public DateTime KardexDate { get; set; }

        [Display(Name = "Tipo de documento")]
        public Int64 DocType { get; set; }

        [Display(Name = "Documento")]
        public Int64? DocumentId { get; set; }

        public int? DocumentLine { get; set; }

        [Display(Name = "Documento")]
        public string DocumentName { get; set; }

        [Display(Name = "Fecha de Kardex")]
        public DateTime DocumentDate { get; set; }


        [Display(Name = "Entrada/Salida")]
        public TipoOperacion? TypeOperationId { get; set; }

        [Display(Name = "Entrada/Salida")]
        public string TypeOperationName { get; set; }       


        [Display(Name = "Sucursal")]
        public Int64? BranchId { get; set; }

        [Display(Name = "Sucursal")]
        public string BranchName { get; set; }

        [Display(Name = "Bodega")]
        public Int64? WareHouseId { get; set; }

        [Display(Name = "Bodega")]
        public string WareHouseName { get; set; }

        [Display(Name = "Estiba")]
        public Int64? Estiba { get; set; }


        [Display(Name = "Servicio")]
        public Int64? ProducId { get; set; }

        [Display(Name = "Producto")]
        public string ProductName { get; set; }

        [Display(Name = "Producto")]
        public Int64? SubProducId { get; set; }

        [Display(Name = "Producto")]
        public string SubProductName { get; set; }

        [Display(Name = "Unidad de medida")]
        public Int64? UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Saldo Anterior")]
        public decimal? SaldoAnterior { get; set; }


        [Display(Name = "Entrada")]
        public decimal QuantityEntry { get; set; }

        [Display(Name = "Salida")]
        public decimal QuantityOut { get; set; }

        [Display(Name = "Saldo")]
        public decimal Total { get; set; }

        [Display(Name = "Entrada de sacos")]
        public decimal QuantityEntryBags { get; set; }

        [Display(Name = "Salida de sacos")]
        public decimal QuantityOutBags { get; set; }

        [Display(Name = "Saldo sacos")]
        public decimal TotalBags { get; set; }

        public bool MaxKardex { get; set; } = true;

        public KardexTypes KardexTypeId { get; set; }

        //public List<KardexLine> _KardexLine { get; set; }


    }


    public class KardexDTO : Kardex
    {
       public List<Int64> Ids { get; set; } = new List<long>();
       public Int64 SalesOrderId { get; set; }

        [Display(Name = "Fecha de inicio")]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Fecha de fin")]
        public DateTime? EndDate { get; set; }

    }


    public enum KardexTypes {
        InventarioFisico = 1,
        PendienteCertificar = 2,
        MercaderriaCertificada = 3,
        MercaderiaAutorizadaSalida = 4,
        MercaderiaEndosada = 5
    
    }

    public enum TipoOperacion { 
        Entrada = 1,
        Salida = 2,
        Ajuste = 3
    }

}
