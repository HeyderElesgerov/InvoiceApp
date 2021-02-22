using InvoiceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Application.ViewModels.Invoice
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public decimal NetAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public string Note { get; set; }

        public InvoiceStatus Status { get; set; }

        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public int ProjectId { get; set; }

        public string ProjectName { get; set; }

        public InvoiceViewModel()
        {
        }

        public InvoiceViewModel(int id, DateTime invoiceDate, DateTime dueDate, decimal netAmount, decimal taxAmount, string note, InvoiceStatus status, Domain.Models.Client client, Domain.Models.Project project)
        {
            Id = id;
            InvoiceDate = invoiceDate;
            DueDate = dueDate;
            NetAmount = netAmount;
            TaxAmount = taxAmount;
            Note = note;
            Status = status;
            ClientName = client != null ? client.FirstName + " " + client.LastName : "";
            ProjectName = project != null ? project.Name : "";
        }
    }
}
