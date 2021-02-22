using AutoMapper;
using FluentValidation.Results;
using InvoiceApp.Application.Interfaces;
using InvoiceApp.Application.ViewModels.Invoice;
using InvoiceApp.Domain.Commands.Invoice;
using InvoiceApp.Domain.Models;
using InvoiceApp.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public InvoiceService(IInvoiceRepository invoiceRepository, IMediator mediator, IMapper mapper)
        {
            _invoiceRepository = invoiceRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<ValidationResult> Add(InvoiceViewModel invoiceViewModel)
        {
            return await _mediator.Send(_mapper.Map<InvoiceViewModel, CreateInvoiceCommand>(invoiceViewModel));
        }

        public decimal CalculateTotalAmount(IEnumerable<InvoiceViewModel> invoiceViewModels)
        {
            return CalculateTotalNetAmount(invoiceViewModels) + CalculateTotalTaxAmount(invoiceViewModels); 
        }

        public decimal CalculateTotalNetAmount(IEnumerable<InvoiceViewModel> invoiceViewModels)
        {
            decimal netSum = 0;

            foreach(var invoice in invoiceViewModels)
            {
                netSum += invoice.NetAmount;
            }

            return netSum;
        }

        public decimal CalculateTotalTaxAmount(IEnumerable<InvoiceViewModel> invoiceViewModels)
        {
            decimal taxSum = 0;

            foreach (var invoice in invoiceViewModels)
            {
                taxSum += invoice.TaxAmount;
            }

            return taxSum;
        }

        public async Task<IEnumerable<InvoiceViewModel>> GetAllIncludingClientAndProject(int? projectId, int? clientid, DateTime? from, DateTime? to)
        {
            var invoices =  await _invoiceRepository.GetWhereIncluding(
               i => 
                (projectId != null ? i.ProjectId == projectId : true) &&
                (clientid != null ? i.ClientId == clientid : true) &&
                (from != null ? i.InvoiceDate >= from : true) &&
                (to != null? i.DueDate <= to : true), 
               i => i.Client, i => i.Project);


            var invoiceVMs = new List<InvoiceViewModel>();

            foreach(var invoice in invoices)
            {
                invoiceVMs.Add(_mapper.Map<Invoice, InvoiceViewModel>(invoice));
            }

            return invoiceVMs;
        }

        public async Task<IEnumerable<InvoiceViewModel>> GetAllIncludingClientAndProjectByProject(int projectId)
        {
            return _mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceViewModel>>(await _invoiceRepository.GetWhereIncluding(i => i.ProjectId == projectId, i => i.Client, i => i.Project));
        }

        public async Task<IEnumerable<InvoiceViewModel>> GetAllIncludingClientAndProjectByClient(int clientId)
        {
            return _mapper.Map<IEnumerable<Invoice>, IEnumerable<InvoiceViewModel>>(await _invoiceRepository.GetWhereIncluding(i => i.ProjectId == clientId, i => i.Client, i => i.Project));
        }



        public async Task<ValidationResult> Update(InvoiceViewModel invoiceViewModel)
        {
            return await _mediator.Send(_mapper.Map<InvoiceViewModel, UpdateInvoiceCommand>(invoiceViewModel));
        }

        public InvoiceViewModel Get(int id)
        {
            return _mapper.Map<Invoice, InvoiceViewModel>(_invoiceRepository.Find(id));
        }
    }
}
