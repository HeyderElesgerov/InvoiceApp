using FluentValidation.Results;
using InvoiceApp.Application.ViewModels.Invoice;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Interfaces
{
    public interface IInvoiceService
    {
        Task<IEnumerable<InvoiceViewModel>> GetAllIncludingClientAndProject(int? projectId = null, int? clientid = null, DateTime? from = null, DateTime? to = null);
        InvoiceViewModel Get(int id);
        decimal CalculateTotalNetAmount(IEnumerable<InvoiceViewModel> invoiceViewModels);
        decimal CalculateTotalTaxAmount(IEnumerable<InvoiceViewModel> invoiceViewModels);
        decimal CalculateTotalAmount(IEnumerable<InvoiceViewModel> invoiceViewModels);
        Task<ValidationResult> Add(InvoiceViewModel clientViewModel);
        Task<ValidationResult> Update(InvoiceViewModel clientViewModel);
    }
}
