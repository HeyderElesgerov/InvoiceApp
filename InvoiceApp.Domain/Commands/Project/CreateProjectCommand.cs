using InvoiceApp.Domain.Commands.Project.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Project
{
    public class CreateProjectCommand : ProjectCommand
    {
        public CreateProjectCommand(string projectName) : base(projectName)
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new CreateProjectCommandValidator().Validate(this);
            return base.IsValid();
        }
    }
}
