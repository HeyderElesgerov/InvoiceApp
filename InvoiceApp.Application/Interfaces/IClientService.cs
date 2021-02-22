using FluentValidation.Results;
using InvoiceApp.Application.ViewModels.Client;
using InvoiceApp.Application.ViewModels.Project;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Interfaces
{
    public interface IClientService
    {
        Task<ClientViewModel> Get(int id);
        Task<IEnumerable<ClientViewModel>> GetAll();
        Task<ValidationResult> Add(ClientViewModel clientViewModel);
        Task<ValidationResult> Update(ClientViewModel clientViewModel);
        Task<ValidationResult> Delete(ClientViewModel clientViewModel);
    }
}
