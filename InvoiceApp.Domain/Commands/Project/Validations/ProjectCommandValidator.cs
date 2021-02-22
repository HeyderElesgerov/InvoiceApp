using FluentValidation;
using InvoiceApp.Domain.Core.CommandBase;
using InvoiceApp.Domain.Core.ErrorConst;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Project.Validations
{
    public class ProjectCommandValidator<TCommand> : CommandValidator<TCommand> 
        where TCommand : ProjectCommand
    {
        public void ValidateName()
        {
            RuleFor(p => p.Project.Name)
                .NotEmpty().WithMessage(
                ErrorMessageGenerator.Generate("Project name", ValidationErrors.IsRequired));
        }
    }
}
