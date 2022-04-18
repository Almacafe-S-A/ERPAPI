using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERPAPI.Models
{
    public class DisponibleBodegaHabilitada
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("InventarioBodegaHabilitadaId ")]
        public InventarioBodegaHabilitada InventarioBodegaHabilitada { get; set; }
        
        public int InventarioBodegaHabilitadaId { get; set; }

        public int ProductId  { get; set; }

        public string PrductName { get; set; }

        public decimal Existencias  { get; set; }

        public decimal IngersoHoy { get; set; }

        public decimal InventarioFisico { get; set; }

        public decimal Retencion7 { get; set; }

        public decimal Merma { get; set; }

        public decimal DisponibleCertificar { get; set; }

        public decimal Certificado { get; set; }


    }
}
