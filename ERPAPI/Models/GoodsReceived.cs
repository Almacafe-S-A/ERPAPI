﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GoodsReceived
    {
        [Display(Name = "Id")]
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GoodsReceivedId { get; set; }

        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }
        [Display(Name = "Fecha")]
        public DateTime OrderDate { get; set; }
        [Display(Name = "Fecha de documento")]
        public DateTime DocumentDate { get; set; }

        [Display(Name = "Sucursal")]
        public Int64 BranchId { get; set; }

        [Display(Name = "Sucursal")]
        public string BranchName { get; set; }


        [Display(Name = "Bodega")]
        public int WarehouseId { get; set; }

        [Display(Name = "Bodega")]
        public string WarehouseName { get; set; }

        [Display(Name = "Producto Cliente")]
        public Int64 SubProductId { get; set; }

        [Display(Name = "Producto Cliente")]
        public string SubProductName { get; set; }

        [Display(Name = "Producto")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Producto")]
        public string ProductName { get; set; }

        [Display(Name = "Recibimos de")]
        public string Name { get; set; }
        [Display(Name = "Referencia")]
        public string Reference { get; set; }

        [Display(Name = "Boleta de salida")]
        public Int64 ExitTicket { get; set; }
        [Display(Name = "Comentarios")]
        public string Comments { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public List<GoodsReceivedLine> _GoodsReceivedLine = new List<GoodsReceivedLine>();

    }
}