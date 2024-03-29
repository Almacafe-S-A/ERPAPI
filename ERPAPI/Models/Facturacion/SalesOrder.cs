﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class SalesOrder
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalesOrderId { get; set; }
        [Display(Name = "Nombre cotizacion")]
        public string SalesOrderName { get; set; }

        [Display(Name = "Tipo de contrato")]
        public Int64 TypeContractId { get; set; }

        [Display(Name = "Nombre de contrato")]
        public string NameContract { get; set; }

        [Display(Name = "Tipo de Facturación")]
        public Int64 TypeInvoiceId { get; set; }

        [Display(Name = "Tipo de Facturación")]
        public string TypeInvoiceName { get; set; }

        [Display(Name = "RTN")]
        public string RTN { get; set; }

        [Display(Name = "Telefono")]
        public string Tefono { get; set; }

        public string Observacion { get; set; }

        public decimal? IncrementoAnual { get; set; }





        //[EmailAddress(ErrorMessage ="Agregue una direccion de correo valida")]
        [EmailAddress]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Correo invalido")]
        [DataType(DataType.EmailAddress)]
        [Display(Name = "Correo")]
        public string Correo { get; set; }

        [Display(Name = "Direccion")]
        public string Direccion { get; set; }

        [Display(Name = "Id Sucursal")]
        public int BranchId { get; set; }

        [Display(Name = "Sucursal Nombre")]
        public string BranchName { get; set; }

        [Display(Name = "Cliente Id")]
        public int CustomerId { get; set; }

        [Display(Name = "Nombre Cliente")]
        public string CustomerName { get; set; }

        public string Customer { get; set; }

        public string Representante { get; set; }

        public string FirmaAlmacafeCargo {get; set; }

        public string FirmaAlmacafe { get; set; }


        public string CargoContactoRepresentante { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime ExpirationDate { get; set; }

        public decimal? PlazoMeses { get; set; }
        /// <summary>
        /// Precio Base del proucto a Almacenar
        /// </summary>
        public decimal? PrecioBaseProducto { get; set; }

        public decimal? ComisionMin { get; set; }

        public decimal? ComisionMax { get; set; }

        public decimal? PrecioServicio { get; set; }

        public bool? PolizaPropia { get; set; }

        

        [Display(Name = "Id")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Nombre Producto")]
        public string ProductName { get; set; }

        [Display(Name = "Unidad de Medida")]
        public Int64 UnitOfMeasureId { get; set; }
        [ForeignKey("UnitOfMeasureId")]
        [Display(Name = "Unidad de Medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Numero de referencia de cliente")]
        public string CustomerRefNumber { get; set; }
        [Display(Name = "Tipo de ventas")]
        public int SalesTypeId { get; set; }
        public string Remarks { get; set; }       

        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }

        public string Impreso { get; set; }

        public Int64? CustomerContractId_Source { get; set; }
        [ForeignKey("CustomerContractId_Source")]
        public CustomerContract CustomerContractSource { get; set; }

        public List<SalesOrderLine> SalesOrderLines { get; set; } = new List<SalesOrderLine>();

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}
