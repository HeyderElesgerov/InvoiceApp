using FluentValidation.Results;
using InvoiceApp.Domain.Core.CommandBase;
using InvoiceApp.Domain.Core.ErrorConst;
using InvoiceApp.Domain.Repository;
using InvoiceApp.Domain.UnitOfWork;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Commands.Invoice
{
    public class InvoiceCommandHandler : CommandHandler,
        IRequestHandler<CreateInvoiceCommand, ValidationResult>,
        IRequestHandler<UpdateInvoiceCommand, ValidationResult>,
        IRequestHandler<DeleteInvoiceCommand, ValidationResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public InvoiceCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ValidationResult> Handle(CreateInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            int clientId = request.Invoice.ClientId;
            int projectId = request.Invoice.ProjectId;

            bool clientExists = await _unitOfWork.ClientRepository.Exists(clientId);
            bool projectExists = await _unitOfWork.ProjectRepository.Exists(projectId);

            if (clientExists && projectExists)
            {
                await _unitOfWork.InvoiceRepository.Add(request.Invoice);
                await _unitOfWork.Commit();
            }
            else
            {
                if (!clientExists)
                    AddError(ErrorMessageGenerator.Generate("Client", ValidationErrors.NotFound));

                if (!projectExists)
                    AddError(ErrorMessageGenerator.Generate("Project", ValidationErrors.NotFound));
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(UpdateInvoiceCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            int id = request.Invoice.Id;
            var existingInvoice = _unitOfWork.InvoiceRepository.Find(id);

            if (existingInvoice != null)
            {
                if(request.Invoice.ClientId != existingInvoice.ClientId)
                {
                    existingInvoice.ClientId = request.Invoice.ClientId;
                    bool clientExists = await _unitOfWork.ClientRepository.Exists(request.Invoice.ClientId);

                    if (!clientExists)
                        AddError(ErrorMessageGenerator.Generate("Client", ValidationErrors.NotFound));
                }
                if(request.Invoice.ProjectId != existingInvoice.ProjectId)
                {
                    int projectId = request.Invoice.ProjectId;
                    existingInvoice.ProjectId = projectId;
                    bool projectExists = await _unitOfWork.ProjectRepository.Exists(projectId);

                    if (!projectExists)
                        AddError(ErrorMessageGenerator.Generate("Project", ValidationErrors.NotFound));
                }

                //project and client exists
                if (ValidationResult.IsValid)
                {
                    existingInvoice.InvoiceDate = request.Invoice.InvoiceDate;
                    existingInvoice.DueDate = request.Invoice.DueDate;
                    existingInvoice.NetAmount = request.Invoice.NetAmount;
                    existingInvoice.TaxAmount = request.Invoice.TaxAmount;
                    existingInvoice.Note = request.Invoice.Note;
                    existingInvoice.Status = request.Invoice.Status;

                    await _unitOfWork.InvoiceRepository.Update(existingInvoice);
                    await _unitOfWork.Commit();
                }
               
            }
            else
            {
                AddError(ErrorMessageGenerator.Generate("Invoice", ValidationErrors.NotFound));
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(DeleteInvoiceCommand request, CancellationToken cancellationToken)
        {
            var invoice = _unitOfWork.InvoiceRepository.Find(request.Id);

            if(invoice == null)
            {
                AddError(ErrorMessageGenerator.Generate("Invoice", ValidationErrors.NotFound));
            }
            else
            {
                await _unitOfWork.InvoiceRepository.Delete(invoice);
                await _unitOfWork.Commit();
            }

            return ValidationResult;
        }

    }
}
