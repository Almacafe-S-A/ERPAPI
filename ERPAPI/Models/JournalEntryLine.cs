using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class JournalEntryLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id de linea Diario")]
        public Int64 JournalEntryLineId { get; set; }
        [Display(Name = "Id Entrada diario")]
        public Int64 JournalEntryId { get; set; }

        [Display(Name = "Id Clase Cuenta")]
        public int AccountId { get; set; }
        [Display(Name = "Tipo de Mvimiento")]
        public DrOrCrSide DrCr { get; set; }
        [Display(Name = "Monto Moviminto")]
        public decimal Amount { get; set; }
        public string Memo { get; set; }
        public virtual JournalEntry JournalEntry { get; set; }
        public virtual Account Account { get; set; }
        [Required]
        [Display(Name = "Usuario de creacion")]
        public string UsuarioCreacion { get; set; }
        [Required]
        [Display(Name = "Usuario de modificacion")]
        public string UsuarioModificacion { get; set; }
        [Required]
        [Display(Name = "Fecha de creacion")]
        public DateTime FechaCreacion { get; set; }
        [Required]
        [Display(Name = "Fecha de modificacion")]
        public DateTime FechaModificacion { get; set; }

    }



}
