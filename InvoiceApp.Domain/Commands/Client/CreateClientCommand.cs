using InvoiceApp.Domain.Commands.Client.Validations;
using InvoiceApp.Domain.Core.CommandBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Client
{
    public class CreateClientCommand : ClientCommand
    {
        public CreateClientCommand(string firstName, string lastName) : base(firstName, lastName)
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateClientCommandValidator().Validate(this);
            return base.IsValid();
        }
    }
}
