using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class VendorProduct
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public Int64 ProductId { get; set; }
        [NotMapped,ForeignKey("ProductId")]
        public Product Product { get; set; }

        public Int64 VendorId { get; set; }
        [NotMapped,ForeignKey("VendorId")]
        public Vendor Vendor { get; set; }




    }
}
