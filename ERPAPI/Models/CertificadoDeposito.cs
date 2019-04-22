using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CertificadoDeposito
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 IdCD { get; set; }

        public Int64 CustomerId { get; set; }

        public Int64 WarehouseId { get; set; }

        public Int64 ServicioId { get; set; }

        public string Direccion { get; set; }

        public DateTime FechaCertificado { get; set; }

        public string NombreEmpresa { get; set; }

        public string EmpresaSeguro { get; set; }

        public string NoPoliza { get; set; }
        public DateTime FechaVencimiento { get; set; }

        public DateTime FechaCreacion { get; set; }

        public  DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }



    }
}
