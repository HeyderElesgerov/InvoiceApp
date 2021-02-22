using InvoiceApp.Domain.Core.CommandBase;
using FluentValidation;

namespace InvoiceApp.Domain.Commands.Invoice.Validations
{
    public abstract class InvoiceCommandValidator<TCommand> : CommandValidator<TCommand> where TCommand : InvoiceCommand
    {
        public void ValidateDates()
        {
            RuleFor(c => c.Invoice.InvoiceDate)
                .LessThanOrEqualTo(c => c.Invoice.DueDate)
                .WithMessage("Invoice Date should be less than or equal to Due Date");
        }

        public void ValidateNetAmount()
        {
            RuleFor(c => c.Invoice.NetAmount)
                .GreaterThan(0).WithMessage("Net Amount should be greater than 0");
        }

        public void ValidateTaxAmount()
        {
            RuleFor(c => c.Invoice.TaxAmount)
                .GreaterThanOrEqualTo(0).WithMessage("Tax Amount should be greater than or equal to 0");
        }
    }
}
