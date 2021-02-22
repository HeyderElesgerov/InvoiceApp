using InvoiceApp.Domain.Core.CommandBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Invoice.Validations
{
    class CreateInvoiceCommandValidator : InvoiceCommandValidator<CreateInvoiceCommand>
    {
        public CreateInvoiceCommandValidator()
        {
            ValidateDates();
            ValidateNetAmount();
            ValidateTaxAmount();
        }
    }
}
