using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

/// <summary>
/// Summary description for Class1
/// </summary>
/// 

namespace ERPAPI.Models
{
    public class BalanceSaldos
    {
        public int? AccountId { get; set; }

        public string AccountCode { get; set; }

        public string Descripcion { get; set; }

        public int? ParentAccountId { get; set; }

        public string DeudoraAcreedora { get; set; }

        public string Estado { get; set; }

        public bool Totaliza { get; set; }

        public double? Debe { get; set; }

        public double? Haber { get; set; }

        public double? AñoActual { get; set; }

    }
}
