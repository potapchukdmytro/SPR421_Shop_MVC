﻿using Microsoft.AspNetCore.Identity;

namespace SPR421_Shop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
