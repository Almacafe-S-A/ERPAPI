using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class KardexType
    {
        public KardexTypes Id { get; set; }

        public string KardexTypeName { get; set; }



    }

    public enum KardexTypes {
        KardexProductoCertificado = 1,
        KardexInventariofisico = 2,
        KardexProductoAutorizadoRetiro = 3,
        KardexProductoEndosado = 4


    
    }
}
