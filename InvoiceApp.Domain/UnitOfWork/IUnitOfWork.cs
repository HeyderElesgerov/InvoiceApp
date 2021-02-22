using InvoiceApp.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IClientRepository ClientRepository { get;  }
        IProjectRepository ProjectRepository { get; }
        IInvoiceRepository InvoiceRepository { get; }

        Task Commit();
    }
}
