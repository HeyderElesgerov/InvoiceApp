using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Client.Validations
{
    class UpdateClientCommandValidator : ClientValidator<UpdateClientCommand>
    {
        public UpdateClientCommandValidator()
        {
            ValidateFirstName();
            ValidateLastName();
        }
    }
}
