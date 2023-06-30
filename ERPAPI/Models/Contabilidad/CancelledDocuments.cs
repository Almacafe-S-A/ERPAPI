using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CancelledDocuments
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Int64 IdTipoDocumento { get; set; }
        [ForeignKey("IdTipoDocumento ")]
        public TiposDocumento TiposDocumento { get; set; }

        public string TipoDocumento { get; set; }

        public int IdDocumento { get; set; }

        public Int64 JournalEntryId { get; set; }
        [ForeignKey("JournalEntryId")]
        public JournalEntry JournalEntry { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }


    }


}
