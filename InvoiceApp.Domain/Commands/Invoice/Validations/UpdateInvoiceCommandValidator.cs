using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Invoice.Validations
{
    class UpdateInvoiceCommandValidator : InvoiceCommandValidator<UpdateInvoiceCommand>
    {
        public UpdateInvoiceCommandValidator()
        {
            ValidateDates();
            ValidateNetAmount();
            ValidateTaxAmount();
        }
    }
}
