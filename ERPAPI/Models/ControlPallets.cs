﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ControlPallets
    {
        [Display(Name = "Estiba Id")]
        public Int64 ControlPalletsId { get; set; }
        public string Motorista { get; set; }

        [Display(Name = "Sucursal")]
        public Int64 BranchId { get; set; }

        [Display(Name = "Estado")]
        public Int64 IdEstado { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [Display(Name = "Bodega")]    
        public int WarehouseId { get; set; }

        [Display(Name = "Fecha control de estiba")]
        public DateTime DocumentDate { get; set; }
        [Display(Name = "Producto")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Producto Cliente")]
        public Int64 SubProductId { get; set; }

        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }      

        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }
        [Display(Name = "Descripción de producto")]
        public string DescriptionProduct { get; set; }
        [Display(Name = "Placas")]
        public string Placa { get; set; }
        public string Marca { get; set; }

        [Display(Name = "Id Estiba")]
        public int PalletId { get; set; }
        public int EsIngreso { get; set; }
        public int EsSalida { get; set; }
        public int SubTotal { get; set; }
        public int TotalSacos { get; set; }
        public int SacosDevueltos { get; set; }
        public double QQPesoBruto { get; set; }
        public double Tara { get; set; }
        public double QQPesoNeto { get; set; }
        public double QQPesoFinal { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public DateTime? FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public List<ControlPalletsLine> _ControlPalletsLine { get; set; } = new List<ControlPalletsLine>();
       // public List<ControlPalletsLine> _ControlPalletsLine = new List<ControlPalletsLine>();

    }
}
