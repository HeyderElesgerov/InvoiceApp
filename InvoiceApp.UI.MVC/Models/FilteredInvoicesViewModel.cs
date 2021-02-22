using InvoiceApp.Application.ViewModels.Client;
using InvoiceApp.Application.ViewModels.Invoice;
using InvoiceApp.Application.ViewModels.Project;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace InvoiceApp.UI.MVC.Models
{
    public class FilteredInvoicesViewModel
    {
        public IEnumerable<InvoiceViewModel> InvoiceViewModels { get; set; }

        public decimal NetSum { get; set; }

        public decimal TaxSum { get; set; }

        public decimal TotalSum
        {
            get
            {
                return NetSum + TaxSum;
            }
        }

        public int? SelectedProjectId { get; set; }

        public int? SelectedClientId { get; set; }

        public List<SelectListItem> Projects { get; set; } = new List<SelectListItem>();

        public List<SelectListItem> Clients { get; set; } = new List<SelectListItem>();

        public DateTime? From { get; set; }

        public DateTime? To { get; set; }

        public FilteredInvoicesViewModel()
        {
        }

        public FilteredInvoicesViewModel(IEnumerable<InvoiceViewModel> invoiceViewModels, decimal net, decimal tax, DateTime? from, DateTime? to, int? clientid, IEnumerable<ClientViewModel> clients, int? projectId, IEnumerable<ProjectViewModel> projects)
        {
            InvoiceViewModels = invoiceViewModels;
            NetSum = net;
            TaxSum = tax;
            From = from;
            To = to;
            SelectedProjectId = projectId;
            SelectedClientId = clientid;
            SetProjects(projects);
            SetClients(clients);
        }

        public void SetProjects(IEnumerable<ProjectViewModel> projects)
        {
            Projects.Add(new SelectListItem("", null, true));
            foreach (var project in projects)
            {
                Projects.Add(new SelectListItem(project.Name, project.Id.ToString(), project.Id == SelectedProjectId));
            }
        }

        public void SetClients(IEnumerable<ClientViewModel> clients)
        {
            Clients.Add(new SelectListItem("", null, true));
            foreach (var client in clients)
            {
                Clients.Add(new SelectListItem(client.FullName, client.Id.ToString(), client.Id == SelectedClientId));
            }
        }
    }
}
