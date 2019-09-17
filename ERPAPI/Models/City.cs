using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class City
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        [Display(Name = "Ciudad")]
        public string Name { get; set; }

        [Display(Name = "Descripción")]
        public string Description { get; set; }

        [Display(Name = "País")]
        public long? CountryId { get; set; }

        [Display(Name = "Departamento")]
        public long? StateId { get; set; }

        [Display(Name = "Empleados")]
        public virtual List<Employees> Employees { get; set; }
    }


}
