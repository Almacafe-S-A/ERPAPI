﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CheckAccountLines
    {
        [Key]
        public int Id { get; set; }

        public Int64 CheckAccountId { get; set; }
        [ForeignKey("CheckAccountId")]
        public CheckAccount CheckAccount { get; set; }

        public string CheckNumber { get; set; }

        public DateTime Date { get; set; }

        public string Place { get; set; }

        public string PaytoOrderOf { get; set; }

        public string RTN { get; set; }

        public int? RetencionId { get; set; }
        [ForeignKey("RetencionId")]
        public RetentionReceipt Retention { get; set; }
        [Column(TypeName = "Money")]
        public decimal Ammount { get; set; }

        public string AmountWords { get; set; }

        public string Sinopsis { get; set; }

        public string Address { get; set; }

        public string Estado { get; set; }

        public Int64 IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estados Estados { get; set; }

        public bool? Impreso { get; set; }

        
        public long? JournalEntrId { get; set; }

        [ForeignKey("JournalEntrId")]
        public JournalEntry JournalEntry { get; set; }

        public  int? PartyTypeId { get; set; }

        public int? PartyId { get; set; }


        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }

    
}
