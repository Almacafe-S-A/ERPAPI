using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InventarioFisico
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }


        public DateTime Fecha { get; set; }

        public DateTime? FechaCompletado { get; set; }

        public int? BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }
        public string  Sucursal { get; set; }

        public Int64? ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

        public string Servicio { get; set; }


        public Int64? CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public string Cliente { get; set; }

        public bool? Control { get; set; }

        public Int64? EstadoId { get; set; }
        [ForeignKey("EstadoId")]
        public Estados Estado { get; set; }

        public string EstadoName { get; set; }

        public string Comentarios { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaModificion { get; set; }

        public string UsuarioCreacion { get; set; }

        public string UsuarioModificacion { get; set; }

        public List<InventarioFisicoLine> InventarioFisicoLines { get; set; }

        public List<InventarioBodegaHabilitada> InventarioBodegaHabilitadaLines { get; set; }



    }
}
