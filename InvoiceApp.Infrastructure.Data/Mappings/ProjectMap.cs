using InvoiceApp.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Infrastructure.Data.Mappings
{
    class ProjectMap : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder
                .HasMany(p => p.Invoices)
                .WithOne(i => i.Project)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
