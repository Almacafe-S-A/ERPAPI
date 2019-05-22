using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Helpers
{
    public class Fechas
    {
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }

        public Int64 Id { get; set; }
    }

    public partial class FacturacionMensual
    {
        public Nullable<int> FacturacionID { get; set; }
        public Nullable<double> Facturacion { get; set; }
        public System.DateTime Date { get; set; }
    }
}
