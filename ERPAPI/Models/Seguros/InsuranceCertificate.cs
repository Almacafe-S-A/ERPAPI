﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ERPAPI.Models
{
    public class InsuranceCertificate
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public int BranchId { get; set; }
        [ForeignKey("BranchId")]
        public Branch Branch { get; set; }

        public Int64 CustomerId { get; set; }

        public string CustomerName { get; set; }

        [ForeignKey("CustomerId")]
        public Customer Customer { get; set; }

        public int InsuranceId { get; set; }
        [ForeignKey("InsuranceId")]
        public Insurances Insurace { get; set; }

        public string InsuranceName { get; set; }

        public string InsurancePolicyNumber { get; set; }

        public string DatePlace { get; set; }

        public Int64? ServicioId { get; set; }
        [ForeignKey("ServicioId")]
        public Product Servicio { get; set; }

        public string ServicioName { get; set; }


        public decimal Amount { get; set; }

        public string AmountWords { get; set; }

        public Int64? ProductTypeId { get; set; }
        [ForeignKey("ProductTypeId")]
        public ProductType ProductType { get; set; }

        public string ProductTypes { get; set; }

        public DateTime Date { get; set; }

        public DateTime DueDate { get; set; }

        public Int64 EstadoId { get; set; }
        [ForeignKey("EstadoId")]
        public Estados Estados { get; set; }

        public DateTime FechaCreacion { get; set; }
        public DateTime FechaModificacion { get; set; }
        public string UsuarioCreacion { get; set; }
        public string UsuarioModificacion { get; set; }



        public List<InsuranceCertificateLine> InsuranceCertificaLines { get; set; }

    }
}
