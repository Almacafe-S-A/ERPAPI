using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class UserToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }

        public Int64 BranchId { get; set; }

        public DateTime LastPasswordChangedDate { get; set; }

        public Int32 Passworddias { get; set; }

        [Display(Name = "Habilitado")]
        public bool? IsEnabled { get; set; }

    }


}
