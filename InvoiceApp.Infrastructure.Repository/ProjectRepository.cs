using InvoiceApp.Domain.Models;
using InvoiceApp.Domain.Repository;
using InvoiceApp.Infrastructure.Data.Context;

namespace InvoiceApp.Infrastructure.Repository
{
    public class ProjectRepository : Repository<Project, int>, IProjectRepository
    {
        public ProjectRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
