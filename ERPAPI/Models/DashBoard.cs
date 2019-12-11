/********************************************************************************************************
-- NAME   :  DashBoard
-- PROPOSE:  show methods DashBoard
REVISIONS:
version              Date                Author                        Description
----------           -------------       ---------------               -------------------------------
1.0                  11/12/2019          Marvin.Guillen                    Creation of Model
********************************************************************************************************/

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class DashBoard
    {
        [Display(Name = "Fecha de Inicio")]
        public DateTime BeginDate { get; set; }


        [Display(Name = "Fecha de Fin")]
        public DateTime EndDate { get; set; }

        [Display(Name = "Fecha de creacion")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Fecha de Modificacion")]
        public DateTime ModifiedDate { get; set; }

    }
}
