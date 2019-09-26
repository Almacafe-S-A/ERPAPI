﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ConciliacionLinea
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public int ConciliacionLineaId { get; set; }



        [ForeignKey("ElementoConfiguracion")]
        public ElementoConfiguracion TipoTransaccion { get; set; }

        [Required]
        [Display(Name = "Monto")]
        public Double Monto { get; set; }


        [Required]
        [Display(Name = "ReferenciaBancaria")]
        public string ReferenciaBancaria { get; set; }

        [ForeignKey("IdMoneda")]
        public Currency Moneda { get; set; }

        [Required]
        [Display(Name = "MonedaName")]
        public string MonedaName { get; set; }


        [Required]
        [Display(Name = "FechaCreacion")]
        public DateTime FechaCreacion { get; set; }

        [Required]
        [Display(Name = "FechaModificacion")]
        public DateTime FechaModificacion { get; set; }

        [Required]
        [Display(Name = "UsuarioCreacion")]
        public string UsuarioCreacion { get; set; }

        [Required]
        [Display(Name = "UsuarioModificacion")]
        public string UsuarioModificacion { get; set; }
    }
}