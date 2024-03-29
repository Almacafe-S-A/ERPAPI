﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CierresJournalEntryLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 CierresJournalEntryLineId { get; set; }

        [Display(Name = "Id de linea Diario")]
        public Int64 JournalEntryLineId { get; set; }
        [Display(Name = "Id Entrada diario")]
        public Int64 JournalEntryId { get; set; }
        [StringLength(30)]
        [Display(Name = "Numero de Centro de Costo")]
        public Int64 CostCenterId { get; set; }

        [Display(Name = "Centro de Costos")]
        public string CostCenterName { get; set; }

        [StringLength(60)]
        [Display(Name = "Nombre de Centro Costo")]
        public string Description { get; set; }

        [Display(Name = "Id Clase Cuenta")]
        public int AccountId { get; set; }

        [Display(Name = "Cuenta")]
        public string AccountName { get; set; }
        //[Display(Name = "Tipo de Movimiento")]
        // public DrOrCrSide DrCr { get; set; }
        [Display(Name = "Débito")]
        public decimal Debit { get; set; }
        [Display(Name = "Crédito")]
        public decimal Credit { get; set; }

        [Display(Name = "Débito moneda del sistema ")]
        public decimal DebitSy { get; set; }
        [Display(Name = "Crédito moneda del sistema")]
        public decimal CreditSy { get; set; }

        [Display(Name = "Débito moneda extranjera ")]
        public decimal DebitME { get; set; }
        [Display(Name = "Crédito moneda extranjera")]
        public decimal CreditME { get; set; }

        public string Memo { get; set; }

        public DateTime FechaCierre { get; set; }

        public int BitacoraCierreContableId { get; set; }

        [ForeignKey("BitacoraCierreContableId")]
        public BitacoraCierreContable BitacoraCierreContable { get; set; }
        [Required]
        [Display(Name = "Usuario de creacion")]
        public string CreatedUser { get; set; }
        [Required]
        [Display(Name = "Usuario de modificacion")]
        public string ModifiedUser { get; set; }
        [Required]
        [Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Display(Name = "Fecha de Modificación")]
        public DateTime ModifiedDate { get; set; }

    }
}
