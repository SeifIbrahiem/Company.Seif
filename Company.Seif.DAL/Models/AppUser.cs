﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Seif.DAL.Models
{
    public class AppUser : IdentityUser
    {

           public string FirstName { get; set; }
           public string LastName { get; set; }
           public bool IsAgree { get; set; }   


    }
}
