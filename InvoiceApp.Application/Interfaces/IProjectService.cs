using FluentValidation.Results;
using InvoiceApp.Application.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectViewModel> Get(int projectId);
        Task<IEnumerable<ProjectViewModel>> GetAll();
        Task<ValidationResult> Add(ProjectViewModel projectViewModel);
        Task<ValidationResult> Update(ProjectViewModel projectViewModel);
        Task<ValidationResult> Delete(ProjectViewModel projectViewModel);
    }
}
