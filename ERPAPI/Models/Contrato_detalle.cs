using ERPAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{

    public class Contrato_detalle
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id de Contrato Detalle")]
        public Int64 Contrato_detalleId { get; set; }
        

        [Display(Name = "Contrato Id ")]
        public Int64 ContratoId { get; set; }

        [ForeignKey("ContratoId")]
        public Contrato Contrato { get; set; }

        [Display(Name = "Id del Producto")]
        public Int64 ProductId { get; set; }

        [Display(Name = "Cantidad")]
        public double Cantidad { get; set; }

        [Display(Name = "Precio")]
        public double Precio { get; set; }
        [Display(Name = "Monto")]
        public double Monto { get; set; }

        //[Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Serie { get; set; }

      //  [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string Modelo { get; set; }

        /// <summary>
        ///  Auditoria 
        /// </summary>
        [Required]
        [Display(Name = "Usuario de creacion")]
        public string UsuarioCreacion { get; set; }
        [Required]
        [Display(Name = "Usuario de modificacion")]
        public string UsuarioModificacion { get; set; }
        [Required]
        [Display(Name = "Fecha de creación")]
        public DateTime CreatedDate { get; set; }
        [Required]
        [Display(Name = "Fecha de Modificación")]
        public DateTime ModifiedDate { get; set; }



    }
}
