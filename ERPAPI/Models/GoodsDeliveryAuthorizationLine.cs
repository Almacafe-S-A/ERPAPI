using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class GoodsDeliveryAuthorizationLine
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 GoodsDeliveryAuthorizationLineId { get; set; }
        public Int64 GoodsDeliveryAuthorizationId { get; set; }
        public Int64 NoCertificadoDeposito { get; set; }
        public double Quantity { get; set; }
        public string Description { get; set; }
        public double valorcertificado { get; set; }
        public double valorfinanciado { get; set; }




    }
}
