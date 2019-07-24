using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class CDGoodsDeliveryAuthorization
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Display(Name = "Id")]
        public Int64 CDGoodsDeliveryAuthorizationId { get; set; }
        [Display(Name = "Recibo de mercaderia")]
        public Int64 CD { get; set; }
        public Int64 GoodsDeliveryAuthorizationId { get; set; }
    }
}
