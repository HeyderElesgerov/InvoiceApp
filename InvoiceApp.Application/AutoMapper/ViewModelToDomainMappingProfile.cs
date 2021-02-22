using AutoMapper;
using InvoiceApp.Application.ViewModels.Client;
using InvoiceApp.Application.ViewModels.Invoice;
using InvoiceApp.Application.ViewModels.Project;
using InvoiceApp.Domain.Commands.Client;
using InvoiceApp.Domain.Commands.Invoice;
using InvoiceApp.Domain.Commands.Project;
using InvoiceApp.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Application.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProjectViewModel, CreateProjectCommand>()
                .ConstructUsing((vm) => new CreateProjectCommand(vm.Name));
            CreateMap<ProjectViewModel, UpdateProjectCommand>()
                .ConstructUsing((vm) => new UpdateProjectCommand(vm.Id, vm.Name));
            CreateMap<ProjectViewModel, DeleteProjectCommand>()
                .ConstructUsing((vm) => new DeleteProjectCommand(vm.Id));

            CreateMap<ClientViewModel, CreateClientCommand>()
                .ConstructUsing((vm) => new CreateClientCommand(vm.FirstName, vm.LastName));
            CreateMap<ClientViewModel, UpdateClientCommand>()
                .ConstructUsing((vm) => new UpdateClientCommand(vm.Id, vm.FirstName, vm.LastName));
            CreateMap<ClientViewModel, DeleteClientCommand>()
                .ConstructUsing((vm) => new DeleteClientCommand(vm.Id));

            CreateMap<InvoiceViewModel, CreateInvoiceCommand>()
                .ConstructUsing((vm) => new CreateInvoiceCommand(vm.InvoiceDate, vm.DueDate, vm.NetAmount, vm.TaxAmount, vm.Note, vm.Status, vm.ClientId, vm.ProjectId));
            CreateMap<InvoiceViewModel, UpdateInvoiceCommand>()
                .ConstructUsing((vm) => new UpdateInvoiceCommand(vm.Id, vm.InvoiceDate, vm.DueDate, vm.NetAmount, vm.TaxAmount, vm.Note, vm.Status, vm.ClientId, vm.ProjectId));
            CreateMap<InvoiceViewModel, DeleteInvoiceCommand>()
                .ConstructUsing((vm) => new DeleteInvoiceCommand(vm.Id));
        }
    }
}
