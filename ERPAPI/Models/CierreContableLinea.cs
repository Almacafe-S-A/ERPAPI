using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CierreContableLinea
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int IdLinea { get; set; }

        public int IdBitacoracierreContable { get; set; }

        [ForeignKey("IdBitacoracierreContable")]
        public CierreContable BitacoraCierresContable { get; set; }

        public DateTime FechaCierre { get; set; }

        public int PasoCierre { get; set; }

        public string Proceso { get; set; }

        public string Estatus { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }
    }
}
