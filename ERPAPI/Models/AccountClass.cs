using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public class AccountClass
    {
        public AccountClass()
        {
            Accounts = new HashSet<Account>();
        }

        public string Name { get; set; }
        public string NormalBalance { get; set; }
        public virtual ICollection<Account> Accounts { get; set; }
    }


}
