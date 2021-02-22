using InvoiceApp.Domain.Core.RepositoryBase;
using InvoiceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Repository
{
    public interface IProjectRepository : IRepository<Project, int>
    {
    }
}
