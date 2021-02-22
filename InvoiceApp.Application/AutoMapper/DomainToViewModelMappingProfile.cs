using AutoMapper;
using InvoiceApp.Application.ViewModels.Client;
using InvoiceApp.Application.ViewModels.Invoice;
using InvoiceApp.Application.ViewModels.Project;
using InvoiceApp.Domain.Models;
using System.Collections.Generic;

namespace InvoiceApp.Application.AutoMapper
{
    public class DomainToViewModelMappingProfile : Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<Project, ProjectViewModel>();
            CreateMap<Client, ClientViewModel>();
            CreateMap<Invoice, InvoiceViewModel>()
                .ConstructUsing(i => new InvoiceViewModel(i.Id, i.InvoiceDate, i.DueDate, i.NetAmount, i.TaxAmount, i.Note, i.Status, i.Client, i.Project));
        }
    }
}
