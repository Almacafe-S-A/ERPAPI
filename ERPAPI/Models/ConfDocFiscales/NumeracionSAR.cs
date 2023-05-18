using ERP.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace ERPAPI.Models
{
    public class NumeracionSAR
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
       // [Key]
        public Int64 IdNumeracion { get; set; }

        public Int64 IdCAI { get; set; }

        public string _cai { get; set; }

        public Int64 NoInicio { get; set; }
        public Int64 NoFin { get; set; }
        public DateTime FechaLimite { get; set; }
        public int CantidadOtorgada { get; set; }

        public Int64? Correlativo { get; set; }
        public string SiguienteNumero { get; set; }

        public Int64 BranchId { get; set; }
        public string BranchName { get; set; }

        public Int64 IdPuntoEmision { get; set; }
        public string PuntoEmision { get; set; }
        public Int64 DocTypeId { get; set; }
        public string DocType { get; set; }
        [Display(Name = "Estado")]
        public Int64 IdEstado { get; set; }
        [Display(Name = "Estado")]
        public string Estado { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }

        public string GetCorrelativo() {
            if (this.Correlativo == null)
            {
                this.Correlativo = Convert.ToInt64(this.NoInicio);
            }

            string correlativo =  $"{this.GetPrefijo()}-{this.Correlativo.ToString().PadLeft(8, '0')}";


            
            this.Correlativo = this.Correlativo+1;


            this.SiguienteNumero = $"{this.GetPrefijo()}-{this.Correlativo}";

            

            return correlativo ;

        }
        public string GetPrefijo()
        {
            return $"000-{this.PuntoEmision}-{this.DocType.Substring(0, 2)}";

        }

        public string getRango() {
            return $"{this.GetPrefijo()}-{this.NoInicio.ToString().PadLeft(8,'0')} al {this.GetPrefijo()}-{this.NoFin.ToString().PadLeft(8, '0')}";
        }

        public NumeracionSAR ObtenerNumeracionSarValida(int tipoDocumento,int sucursal,ApplicationDbContext _context)
        {

            NumeracionSAR numeracionSAR = new NumeracionSAR();
            List<NumeracionSAR> numeracionSARs = new List<NumeracionSAR>();
            DateTime fecha = DateTime.Now;

            numeracionSARs = _context.NumeracionSAR
                    .Where(q => q.DocTypeId == tipoDocumento
                    && q.BranchId== sucursal
                    && fecha < q.FechaLimite
                    && (q.Correlativo <= q.NoFin || q.SiguienteNumero == null || q.Correlativo == null)
                    && q.IdEstado == 1)
                    .ToList();

            if (numeracionSARs.Count == 0)
            {
                Exception exception = new Exception("No existe numeracion valida");
                throw exception;
            }
            if (numeracionSARs.Count > 1)
            {
                Exception exception = new Exception("Se encontro mas de una numeracion valida");
                throw exception;
            }


            numeracionSAR = numeracionSARs.FirstOrDefault();

            return numeracionSAR;


        }



    }
}
