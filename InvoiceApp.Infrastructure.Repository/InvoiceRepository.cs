using InvoiceApp.Domain.Models;
using InvoiceApp.Domain.Repository;
using InvoiceApp.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Infrastructure.Repository
{
    public class InvoiceRepository : Repository<Invoice, int>, IInvoiceRepository
    {
        public InvoiceRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
