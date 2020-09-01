using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class MatrizRiesgoCustomers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }
        public Int64 CustomerId { get; set; }
        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }
        public int BranchId { get; set; }
        public string BranchName { get; set; }
        public Int64 ProductId { get; set; }
        [ForeignKey("ProductId")]
        public Product Product { get; set; }
        public Int64 IdFactorRiesgo { get; set; }
        public string FactorRiesgo { get; set; }
        public string Riesgo { get; set; }
        public string Efecto { get; set; }
        public Int64 IdContextoRiesgo { get; set; }
        [ForeignKey("IdContextoRiesgo")]
        public ContextoRiesgo ContextoRiesgo { get; set; }
        public string Responsable { get; set; }
        public Int64 RiesgoInicialProbabilidad { get; set; }
        public Int64 RiesgoInicialImpacto { get; set; }
        public Int64 RiesgoInicialCalificacion { get; set; }
        public Int64 RiesgoInicialValorSeveridad { get; set; }
        public string RiesgoInicialNivel { get; set; }
        public string RiesgoInicialColorHexadecimal { get; set; }
        public string Controles { get; set; }
        public int TipodeAccionderiesgoId { get; set; }
        [ForeignKey("TipodeAccionderiesgoId")]
        public TipodeAccionderiesgo TipodeAccionderiesgo { get; set; }
        public string FechaObjetvo { get; set; }
        public Int64 RiesgoResidualProbabilidad { get; set; }
        public Int64 RiesgoResidualImpacto { get; set; }
        public Int64 RiesgoResidualCalificacion { get; set; }
        public Int64 RiesgoResidualValorSeveridad { get; set; }
        public string RiesgoResidualNivel { get; set; }
        public string RiesgoResidualColorHexadecimal { get; set; }
        public string Seguimiento { get; set; }
        public DateTime FechaRevision { get; set; }
        public double Avance { get; set; }
        public bool Eficaz { get; set; }
        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }
    }
}