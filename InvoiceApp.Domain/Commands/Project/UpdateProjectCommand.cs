using InvoiceApp.Domain.Commands.Project.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Project
{
    public class UpdateProjectCommand : ProjectCommand
    {
        public UpdateProjectCommand(int projectId, string projectName) : base(projectId, projectName)
        {
        }

        public override bool IsValid()
        {
            ValidationResult = new UpdateProjectCommandValidator().Validate(this);
            return base.IsValid();
        }
    }
}
