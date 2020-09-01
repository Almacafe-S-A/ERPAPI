using ERPAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Models
{
    public partial class Party 
    {
        public Party()
        {
            //Contacts = new HashSet<Contact>();
        }

        public Int64 PartyId { get; set; }

        public PartyTypes PartyType { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public bool IsActive { get; set; }

      //  public virtual ICollection<Contact> Contacts { get; set; }
    }
}
