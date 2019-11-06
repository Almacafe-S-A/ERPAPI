using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPAPI.Models
{
    public class SeveridadRiesgo
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 IdSeveridad { get; set; }

        [Required]
        public Int64 Impacto { get; set; }
        [Required]
        public Int64 Probabilidad { get; set; }
        [Required]
        public Int64 LimiteCalidadInferior { get; set; }
        [Required]
        public Int64 LimeteCalidadSuperir { get; set; }
        public double RangoInferiorSeveridad { get; set; }
        public double RangoSuperiorSeveridad { get; set; }
        public string Nivel { get; set; }
        public string ColorHexadecimal { get; set; }
    }
}
