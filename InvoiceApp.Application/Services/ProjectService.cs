using AutoMapper;
using FluentValidation.Results;
using InvoiceApp.Application.Interfaces;
using InvoiceApp.Application.ViewModels.Project;
using InvoiceApp.Domain.Commands.Project;
using InvoiceApp.Domain.Models;
using InvoiceApp.Domain.Repository;
using MediatR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvoiceApp.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProjectService(IProjectRepository projectRepository, IMediator mediator, IMapper mapper)
        {
            _projectRepository = projectRepository;
            _mediator = mediator;
            _mapper = mapper;
        }

        public Task<ValidationResult> Add(ProjectViewModel projectViewModel)
        {
            var createProjectCommand = _mapper.Map<ProjectViewModel, CreateProjectCommand>(projectViewModel);
            return _mediator.Send(createProjectCommand);
        }

        public Task<ValidationResult> Delete(ProjectViewModel projectViewModel)
        {
            var deleteProjectCommand = _mapper.Map<ProjectViewModel, DeleteProjectCommand>(projectViewModel);
            return _mediator.Send(deleteProjectCommand);
        }

        public Task<ProjectViewModel> Get(int projectId)
        {
            Project project = _projectRepository.Find(projectId);
            return Task.FromResult(_mapper.Map<Project, ProjectViewModel>(project));
        }

        public async Task<IEnumerable<ProjectViewModel>> GetAll()
        {
            var projects = await _projectRepository.GetAll();
            var projectVms = new List<ProjectViewModel>();

            foreach(var project in projects)
            {
               projectVms.Add(_mapper.Map<Project, ProjectViewModel>(project));
            }

            return projectVms;
        }

        public Task<ValidationResult> Update(ProjectViewModel projectViewModel)
        {
            var updateProjectCommand = _mapper.Map<ProjectViewModel, UpdateProjectCommand>(projectViewModel);
            return _mediator.Send(updateProjectCommand);
        }
    }
}
