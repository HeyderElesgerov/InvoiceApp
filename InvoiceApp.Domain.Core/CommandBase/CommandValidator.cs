using System;
using System.Collections.Generic;
using System.Text;
using FluentValidation;

namespace InvoiceApp.Domain.Core.CommandBase
{
    public abstract class CommandValidator<TCommand> : AbstractValidator<TCommand> where TCommand : Command
    {
    }
}
