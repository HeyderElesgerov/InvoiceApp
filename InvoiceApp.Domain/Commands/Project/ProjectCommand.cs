using InvoiceApp.Domain.Core.CommandBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Project
{
    public abstract class ProjectCommand : Command
    {
        public Models.Project Project { get; private set; }

        public ProjectCommand(string projectName)
        {
            Project = new Models.Project(projectName);
        }

        public ProjectCommand(int projectId, string projectName)
        {
            Project = new Models.Project(projectId, projectName);
        }
    }
}
