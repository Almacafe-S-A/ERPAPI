using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ERPAPI.Models
{
    public class Auditoria
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)] 
        public long Id { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime Fecha { get; set; }
        [MaxLength(100)]
        public string Usuario { get; set; }
        [MaxLength(200)]
        public string Entidad { get; set; }
        [MaxLength(200)]
        public string Llave { get; set; }
        [MaxLength(10)]
        public string Accion { get; set; }
        [MaxLength(int.MaxValue)]
        public string ValoresNuevos { get; set; }
        [MaxLength(int.MaxValue)]
        public string ValoresAntiguos { get; set; }
        
    }
}
