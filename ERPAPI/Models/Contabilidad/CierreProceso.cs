using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CierresProceso
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string NombreProceso { get; set; }

        public string Descripcion { get; set; }

        public string Modulo { get; set; }

        public int MesEjecucion { get; set; }

        public Int64 EstadoId { get; set; }

        public string Estatus { get; set; }        

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificacion { get; set; }

        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }

    }
}
