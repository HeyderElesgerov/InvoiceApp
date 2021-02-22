using Microsoft.AspNetCore.Identity;
using System;

namespace InvoiceApp.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string Surname { get; set; }
    }
}
