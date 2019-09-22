﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class JournalEntryConfiguration
    {
         [Display(Name = "Id")]
         public Int64 JournalEntryConfigurationId { get; set; }
         [Display(Name = "Transacción")]
         public Int64 TransactionId { get; set; }
         [Display(Name = "Transacción")]
         public string Transaction { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "Moneda")]
        public Int64 CurrencyId { get; set; }

        [Display(Name = "Moneda")]
        public string CurrencyName { get; set; }

        [Display(Name = "Fecha de creación")]
        public DateTime FechaCreacion { get; set; }

        [Display(Name = "Fecha de modificación")]
        public DateTime FechaModificacion { get; set; }

        [Display(Name = "Usuario de creación")]
        public string UsuarioCreacion { get; set; }
        [Display(Name = "Usuario de modificación")]
        public string UsuarioModificacion { get; set; }
     


        List<JournalEntryConfigurationLine> JournalEntryConfigurationLine = new List<JournalEntryConfigurationLine>();

    }
}