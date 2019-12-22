using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{

    public class Contrato_plan_pagos
    {

        //[Display(Name = "Plan de Pago Id")]
        //public Int64 Contrato_plan_pagosId { get; set; }
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity), Column(Order = 0)]
        [Display(Name = "nro de Cuota")]
        public Int64 Nro_cuota { get; set; }


        [Key, Column(Order = 1)]
        [Display(Name = "Contrato Id ")]
        public Int64 ContratoId { get; set; }

        [ForeignKey("ContratoId")]
        public Contrato Contrato { get; set; }
        [Display(Name = "Fecha de Cuota  ")]
        public DateTime Fechacuota { get; set; }

        [Display(Name = "Valor Capital")]
        public Double Valorcapital { get; set; }

        [Display(Name = "Valor Interes")]
        public Double Valorintereses { get; set; }

        [Display(Name = "Valor Seguros")]
        public Double Valorseguros { get; set; }

   


        [Display(Name = "Interes Moratorios pagados")]
        public Double Interesesmoratorios { get; set; }

        [Display(Name = "Otros Cargos")]
        public Double Valorotroscargos { get; set; }
     [Display(Name = "Estado de Cuota")]
        public Int16 Estadocuota { get; set; }

        [Display(Name = "Valor Pagado")]
        public Double Valorpagado { get; set; }

        [Display(Name = "Fecha de Pago  ")]
        public DateTime Fechapago { get; set; }

        [Display(Name = "Recibo de Pago  ")]
        public String Recibopago { get; set; }

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
