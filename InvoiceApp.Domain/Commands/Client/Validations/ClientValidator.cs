using InvoiceApp.Domain.Core.CommandBase;
using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;
using InvoiceApp.Domain.Core.ErrorConst;

namespace InvoiceApp.Domain.Commands.Client.Validations
{
    public class ClientValidator<TCommand> : CommandValidator<TCommand> where TCommand : ClientCommand
    {
        public void ValidateFirstName()
        {
            RuleFor(c => c.Client.FirstName)
                .NotEmpty()
                    .WithMessage(ErrorMessageGenerator.Generate("First Name", ValidationErrors.IsRequired));
        }

        public void ValidateLastName()
        {
            RuleFor(c => c.Client.LastName)
                .NotEmpty()
                    .WithMessage(ErrorMessageGenerator.Generate("Last Name", ValidationErrors.IsRequired));
        }
    }
}
