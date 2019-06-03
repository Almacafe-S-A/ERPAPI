using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class BlackListCustomers
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 BlackListId { get; set; }
        [Display(Name = "Cliente")]
        public Int64 CustomerId { get; set; }
        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }
        [Display(Name = "Fecha")]
        public DateTime DocumentDate { get; set; }
        [Display(Name = "Alias")]
        public string Alias { get; set; }
        [Display(Name = "Identidad")]
        public string Identidad { get; set; }
        [Display(Name = "RTN")]
        public string RTN { get; set;  }
        [Display(Name = "Referencia")]
        public string Referencia { get; set; }
        [Display(Name = "Origen")]
        public string Origen { get; set; }
        [Display(Name = "Activo")]
        public string Activo { get; set; }



    }
}
