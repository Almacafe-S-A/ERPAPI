using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InsuranceCertificaLine
    {
        public int Id { get; set; }

        public int InsuraceCertificateId { get; set; }
        [ForeignKey("InsuraceCertificateId")]
        public InsuranceCertificate InsuranceCertificate { get; set; }

        public int WarehouseId { get; set; }

        [ForeignKey("WarehouseId")]
        public Warehouse Warehouse { get; set; }

        public decimal Amount { get; set; }

    }
}
