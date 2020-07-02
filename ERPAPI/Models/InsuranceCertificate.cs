﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class InsuranceCertificate
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public Int64 CustomerId { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public int InsuranceId { get; set; }
        [ForeignKey("InsuranceId")]
        public Insurances Insurace { get; set; }

        public decimal Amount { get; set; }

        public string AmountWords { get; set; }

        public Int64? ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

        public string ProductTypes { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }


        public List<InsuranceCertificateLine> InsuranceCertificaLines { get; set; }

    }
}
