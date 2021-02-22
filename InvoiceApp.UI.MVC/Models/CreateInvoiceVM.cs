using InvoiceApp.Application.ViewModels.Client;
using InvoiceApp.Application.ViewModels.Invoice;
using InvoiceApp.Application.ViewModels.Project;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace InvoiceApp.UI.MVC.Models
{
    public class CreateInvoiceVM
    {
        public InvoiceViewModel InvoiceViewModel { get; set; }

        public List<SelectListItem> Projects { get; set; }

        public List<SelectListItem> Clients { get; set; }

        public CreateInvoiceVM()
        {
        }

        public CreateInvoiceVM(InvoiceViewModel invoiceViewModel)
        {
            InvoiceViewModel = invoiceViewModel;
        }

        public void SetProjects(IEnumerable<ProjectViewModel> projects)
        {
            Projects = new List<SelectListItem>();

            foreach(var project in projects)
            {
                Projects.Add(new SelectListItem()
                {
                    Text = project.Name,
                    Value = project.Id.ToString(),
                    Selected = project.Id == InvoiceViewModel.ProjectId
                });
            }
        }

        public void SetClients(IEnumerable<ClientViewModel> clients)
        {
            Clients = new List<SelectListItem>();

            foreach (var client in clients)
            {
                Clients.Add(new SelectListItem()
                {
                    Text = client.FullName,
                    Value = client.Id.ToString(),
                    Selected = client.Id == InvoiceViewModel.ClientId
                });
            }
        }
    }
}
