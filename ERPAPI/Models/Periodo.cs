using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class Periodo
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Anio { get; set; }

        public Int64 IdEstado { get; set; }

        [ForeignKey("IdEstado")]
        public Estados Estados { get; set; }

        public string Estado { get; set; }



    }
}
