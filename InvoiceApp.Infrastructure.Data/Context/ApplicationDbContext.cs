using InvoiceApp.Infrastructure.Data.Mappings;
using InvoiceApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Infrastructure.Data.Context
{
    public class ApplicationDbContext : IdentityDbContext<AppUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ProjectMap());
            builder.ApplyConfiguration(new InvoiceMap());
            builder.ApplyConfiguration(new ClientMap());

            base.OnModelCreating(builder);
        }
    }
}
