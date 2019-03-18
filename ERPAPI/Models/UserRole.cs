using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class UserRole 
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string rol { get; set; }
    }
}
