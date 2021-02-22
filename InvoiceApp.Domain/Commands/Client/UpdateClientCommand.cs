using InvoiceApp.Domain.Commands.Client.Validations;
using InvoiceApp.Domain.Core.CommandBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Client
{
    public class UpdateClientCommand : ClientCommand
    {
        public UpdateClientCommand(int clientId, string firstName, string lastName)
            : base(clientId, firstName, lastName)
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateClientCommandValidator().Validate(this);
            return base.IsValid();
        }
    }
}
