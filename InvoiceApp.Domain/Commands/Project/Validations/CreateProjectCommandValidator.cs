using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Project.Validations
{
    public class CreateProjectCommandValidator : ProjectCommandValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            ValidateName();
        }
    }
}
