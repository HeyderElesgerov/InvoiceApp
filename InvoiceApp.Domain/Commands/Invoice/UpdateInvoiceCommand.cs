using InvoiceApp.Domain.Commands.Invoice.Validations;
using InvoiceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Invoice
{
    public class UpdateInvoiceCommand : InvoiceCommand
    {
        public UpdateInvoiceCommand(int id, DateTime invoiceDate, DateTime dueDate, decimal netAmount, decimal taxAmount, string note, InvoiceStatus status, int clientId, int projectId)
            : base(id, invoiceDate, dueDate, netAmount, taxAmount, note, status, clientId, projectId)
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateInvoiceCommandValidator().Validate(this);
            return base.IsValid();
        }
    }
}
