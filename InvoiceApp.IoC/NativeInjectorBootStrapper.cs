using FluentValidation.Results;
using InvoiceApp.Application.AutoMapper;
using InvoiceApp.Application.Interfaces;
using InvoiceApp.Application.Services;
using InvoiceApp.Domain.Commands.Client;
using InvoiceApp.Domain.Commands.Invoice;
using InvoiceApp.Domain.Commands.Project;
using InvoiceApp.Domain.Repository;
using InvoiceApp.Domain.UnitOfWork;
using InvoiceApp.Infrastructure.Data.Context;
using InvoiceApp.Infrastructure.Identity;
using InvoiceApp.Infrastructure.Repository;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Security.Claims;

namespace InvoiceApp.Infrastructure.IoC
{
    public static class NativeInjectorBootStrapper
    {
        public static IServiceCollection WithAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(
                typeof(DomainToViewModelMappingProfile).Assembly,
                typeof(ViewModelToDomainMappingProfile).Assembly);

            return services;
        }

        public static IServiceCollection WithUnitOfWork(this IServiceCollection serviceDescriptors)
        {
            serviceDescriptors.AddScoped<IUnitOfWork, UnitOfWork>();
            return serviceDescriptors;
        }

        public static IServiceCollection WithRepositories(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IClientRepository, ClientRepository>();
            serviceCollection.AddScoped<IInvoiceRepository, InvoiceRepository>();
            serviceCollection.AddScoped<IProjectRepository, ProjectRepository>();

            return serviceCollection;
        }

        public static IServiceCollection WithServices(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IClientService, ClientService>();
            serviceCollection.AddScoped<IInvoiceService, InvoiceService>();
            serviceCollection.AddScoped<IProjectService, ProjectService>();

            return serviceCollection;
        }

        public static IServiceCollection WithDbContext(this IServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            return serviceCollection;
        }

        public static IServiceCollection WithRequestHandlers(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddMediatR(typeof(NativeInjectorBootStrapper).Assembly);

            serviceCollection.AddScoped<IRequestHandler<CreateClientCommand, ValidationResult>, ClientCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateClientCommand, ValidationResult>, ClientCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<DeleteClientCommand, ValidationResult>, ClientCommandHandler>();

            serviceCollection.AddScoped<IRequestHandler<CreateProjectCommand, ValidationResult>, ProjectCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateProjectCommand, ValidationResult>, ProjectCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<DeleteProjectCommand, ValidationResult>, ProjectCommandHandler>();

            serviceCollection.AddScoped<IRequestHandler<CreateInvoiceCommand, ValidationResult>, InvoiceCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<UpdateInvoiceCommand, ValidationResult>, InvoiceCommandHandler>();
            serviceCollection.AddScoped<IRequestHandler<DeleteInvoiceCommand, ValidationResult>, InvoiceCommandHandler>();

            return serviceCollection;
        }
    }
}
