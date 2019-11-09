using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Warehouse
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int WarehouseId { get; set; }

        [Required]
        [Display(Name = "Nombre del almacén")]
        public string WarehouseName { get; set; }
        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Estado")]
        public Int64 IdEstado { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        [Display(Name = "Sucursal")]
        public int BranchId { get; set; }

        [Display(Name = "Bodega")]
        public double CapacidadBodega { get; set; }

        [Display(Name = "Unidad de medida")]
        public int UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Numero de poliza")]
        public string NoPoliza { get; set; }

        [Display(Name = "Moneda")]
        public Int64 CurrencyId { get; set; }
        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [Display(Name = "Cantidad de la póliza")]
        public double CantidadPoliza { get; set; }

        [Display(Name = "Fecha de emisión poliza")]
        public DateTime FechaEmisionPoliza { get; set; }
        [Display(Name = "Fecha de vencimiento poliza")]
        public DateTime FechaVencimientoPoliza { get; set; }

        [Display(Name = "Cliente")]
        public Int64? CustomerId { get; set; }

        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Fecha libertad de gravamen")]
        //Cada 6 meses -> 1 mes antes
        public DateTime? FechaLibertadGravamen { get; set; }

        [Display(Name = "Fecha habilitación")]
        //Cada dos años -> 3 meses antes, 21 meses transcurridos
        public DateTime? FechaHabilitacion { get; set; }


        [Required]
        [Display(Name = "Usuario de creación")]
        public string UsuarioCreacion { get; set; }
        [Required]
        [Display(Name = "Usuario de modificación")]
        public string UsuarioModificacion { get; set; }
        [Required]
        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }
    }
}
