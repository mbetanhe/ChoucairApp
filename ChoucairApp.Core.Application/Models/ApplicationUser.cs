﻿using Microsoft.AspNetCore.Identity;

namespace ChoucairApp.Core.Application.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Document { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
