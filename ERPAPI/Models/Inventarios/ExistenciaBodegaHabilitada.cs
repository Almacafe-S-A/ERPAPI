using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class ExistenciaBodegaHabilitada
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }

        public InventarioFisico InventarioFisico { get; set; }
        [ForeignKey("InventarioBodegaHabilidaId")]
        public int InventarioFisicoId { get; set; }


        public Int64 SubProductId { get; set; }
        [ForeignKey("SubProductId")]
        public SubProduct SubProduct { get; set; }

        

        public decimal ExistenciaTotal { get; set; }

        public decimal IngresandoHoy { get; set; }

        public decimal DisponibleCertificar { get; set; }

        public decimal RetencionesCafeInferiores { get; set; }

        public decimal Subtotal { get; set; }

        public decimal RetencionesMerma { get; set; }

        public decimal TotalCertificado { get; set; }

        public bool Max { get; set; }






    }
}
