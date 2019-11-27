using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class ConfiguracionesGenerales
    {
        public int Id { get; set; }

        public string Codigo { get; set; }

        public string Origen { get; set; }

        public string NombreProceso { get; set; }

        public string DescripcionProceso { get; set; }

        public string Tipo { get; set; }

        public double ValorGlobal { get; set; }

        public double RangoInicial { get; set; }

        public double RangoFinal { get; set; }

        public Int64 IdEstado { get; set; }
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }


    }
}
