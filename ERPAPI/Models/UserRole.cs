﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ERPAPI.Models
{

    public class ApplicationUserRole : IdentityUserRole<string> 
    {

    }

    //public class  ApplicationUserRole<TKey> : IdentityUserRole<TKey> where TKey : IEquatable<TKey>
    //{

    //}

    public class UserRole 
    {
        public ApplicationUser ApplicationUser { get; set; }
        public string rol { get; set; }
      

    }
}
