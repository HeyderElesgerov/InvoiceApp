using InvoiceApp.Domain.Core.CommandBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Project
{
    public class DeleteProjectCommand : Command
    {
        public int ProjectId;

        public DeleteProjectCommand(int projectId)
        {
            ProjectId = projectId;
        }
    }
}
