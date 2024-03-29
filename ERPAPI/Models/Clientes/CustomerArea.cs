﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CustomerArea
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 CustomerAreaId { get; set; }

        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }

        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "Fecha de documento")]
        public DateTime DocumentDate { get; set; }

        //[Display(Name = "Producto")]
        //public Int64 ProductId { get; set; }

        //[Display(Name = "Producto")]
        //public string ProductName { get; set; }

        [Display(Name = "Unidad de medida")]
        public Int64 UnitOfMeasureId { get; set; }

        [Display(Name = "Unidad de medida")]
        public string UnitOfMeasureName { get; set; }

        [Display(Name = "Sucursal")]
        public Int64 BranchId { get; set; }

        [Display(Name = "Sucursal")]
        public string BranchName { get; set; }

        [Display(Name = "Servicio")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Servicio")]
        public string ProductName { get; set; }


        [Display(Name = "Tipo")]
        public Int64 TypeId { get; set; }

        [Display(Name = "Tipo")]
        public string TypeName { get; set; }

        [Display(Name = "Bodega")]
        public Int64 WareHouseId { get; set; }

        [Display(Name = "Bodega")]
        public string WareHouseName { get; set; }


        public int? InvoiceId { get; set; }
        [ForeignKey("InvoiceId")]
        public Invoice Invoice { get; set; }

        public double Ancho { get; set; }

        public double Alto { get; set; }

        public double Largo { get; set; }

        //[Display(Name = "Estiba")]
        //public Int64 ControlPalletsId { get; set; }

        //public string PalletsId { get; set; }


        [Display(Name = "Area Utilizada")]
        public double UsedArea { get; set; }

       



        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public string Impreso { get; set; }

        public List<CustomerAreaProduct> CustomerAreaProduct { get; set; }

        public bool Cerrado { get; set; }

    }
}
