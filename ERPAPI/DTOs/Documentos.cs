using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.DTOS
{
    public class Documento
    {
        
        public int DocumentoId { get; set; }

        public string DocumentType { get; set; }

        public int DocumentTypeId { get; set; }

        public string NumeroDEI { get; set; }

        public string Sucursal { get; set; }


        public decimal Saldo { get; set; }

        public decimal SaldoImpuesto { get; set; }

        public int? JournalId { get; set; }

        public int CustomerId { get; set; }

        public string CustomerName { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public DateTime? FechaDocumento { get; set; }

        public DateTime? FechaVencimiento { get; set; }

        public Identificador Identificador { get; set; }


        public decimal Total { get; set; }


    }

    public class Identificador {
        public int Id { get; set; }

        public int Tipo { get; set; }
    }
}
