using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    // Add profile data for application users by adding properties to this class (core user identity data)
    public class AppUser : IdentityUser
    {
        // Extended Properties
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
