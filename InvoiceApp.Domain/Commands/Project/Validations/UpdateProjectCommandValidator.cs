﻿using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Project.Validations
{
    public class UpdateProjectCommandValidator : ProjectCommandValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            ValidateName();
        }
    }
}
