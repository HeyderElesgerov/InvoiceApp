using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Client.Validations
{
    class CreateClientCommandValidator : ClientValidator<CreateClientCommand>
    {
        public CreateClientCommandValidator()
        {
            ValidateFirstName();
            ValidateLastName();
        }
    }
}
