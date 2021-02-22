using InvoiceApp.Domain.Repository;
using InvoiceApp.Domain.UnitOfWork;
using InvoiceApp.Infrastructure.Data.Context;
using System.Threading.Tasks;

namespace InvoiceApp.Infrastructure.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        ApplicationDbContext _db;
        IClientRepository _clientRepository;
        IProjectRepository _projectRepository;
        IInvoiceRepository _invoiceRepository;

        public UnitOfWork(ApplicationDbContext db, IClientRepository clientRepository, IProjectRepository projectRepository, IInvoiceRepository invoiceRepository)
        {
            _db = db;
            _clientRepository = clientRepository;
            _projectRepository = projectRepository;
            _invoiceRepository = invoiceRepository;
        }

        public IClientRepository ClientRepository { get => _clientRepository; }
        public IProjectRepository ProjectRepository { get => _projectRepository; }
        public IInvoiceRepository InvoiceRepository { get => _invoiceRepository; }

        public async Task Commit()
        {
            await _db.SaveChangesAsync();
        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
