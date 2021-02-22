using InvoiceApp.Domain.Models;
using InvoiceApp.Domain.Repository;
using InvoiceApp.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Infrastructure.Repository
{
    public class ClientRepository : Repository<Client, int>, IClientRepository
    {
        public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
