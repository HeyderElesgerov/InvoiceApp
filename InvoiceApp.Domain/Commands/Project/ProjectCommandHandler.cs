using FluentValidation.Results;
using InvoiceApp.Domain.Core.CommandBase;
using InvoiceApp.Domain.Core.ErrorConst;
using InvoiceApp.Domain.Repository;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace InvoiceApp.Domain.Commands.Project
{
    public class ProjectCommandHandler :
        CommandHandler,
        IRequestHandler<CreateProjectCommand, ValidationResult>,
        IRequestHandler<UpdateProjectCommand, ValidationResult>,
        IRequestHandler<DeleteProjectCommand, ValidationResult>
    {
        private readonly IProjectRepository _projectRepository;

        public ProjectCommandHandler(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public async Task<ValidationResult> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            await _projectRepository.Add(request.Project);
            await _projectRepository.Commit();

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            if (!request.IsValid()) return request.ValidationResult;

            var project = _projectRepository.Find(request.Project.Id);

            if (project == null)
                AddError(ValidationErrors.NotFound);
            else
            {
                project.ChangeName(request.Project.Name);

                await _projectRepository.Update(project);
                await _projectRepository.Commit();
            }

            return ValidationResult;
        }

        public async Task<ValidationResult> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var project = _projectRepository.Find(request.ProjectId);

            if (project == null)
                AddError(ValidationErrors.NotFound);
            else
            {
                await _projectRepository.Delete(project);
                await _projectRepository.Commit();
            }

            return ValidationResult;
        }
    }
}
