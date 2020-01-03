using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{

    public class Contrato_movimientos
    {

        //[Display(Name = "Plan de Pago Id")]
        //public Int64 Contrato_plan_pagosId { get; set; }, Column(Order = 0)
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Movimiento de Contrato ID ")]
        public Int64 Contrato_movimientosId{ get; set; }


        //[Key, Column(Order = 1)]
        [Display(Name = "Contrato Id ")]
        public Int64 ContratoId { get; set; }

        [ForeignKey("ContratoId")]
        public Contrato Contrato { get; set; }
        [Display(Name = "Fecha de Movimiento  ")]
        public DateTime Fechamovimiento { get; set; }

        [Display(Name = "Sucursal ")]
        public int? BranchId { get; set; }

        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        [Display(Name = "Tipo de Movimiento")]
        public int tipo_movimiento { get; set; }

        [Display(Name = "Valor Movimiento")]
        public Double Valorcapital { get; set; }

        [Display(Name = "Forma de Pago")]
        public int Forma_pago { get; set; }

        [Display(Name = "Id del Empleado ")]
        public Int64? EmployeesId { get; set; }

        [ForeignKey("EmployeesId")]
        public Employees Employees { get; set; }

        // Validar si se puede usar llave foranea a paymentsTypeId


        /// <summary>
        ///  Auditoria 
        /// </summary>
        [Required]
        [Display(Name = "Usuario de creacion")]
        public string UsuarioCreacion { get; set; }
        [Required]
        [Display(Name = "Usuario de modificacion")]
        public string UsuarioModificacion { get; set; }
        [Required]
        [Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Display(Name = "Fecha de Modificación")]
        public DateTime ModifiedDate { get; set; }



    }
}
