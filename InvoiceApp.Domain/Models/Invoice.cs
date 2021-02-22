using InvoiceApp.Domain.Core.Entity;
using InvoiceApp.Domain.Enums;
using System;

namespace InvoiceApp.Domain.Models
{
    public class Invoice : BaseEntity<int>
    {
        public DateTime InvoiceDate { get; set; }

        public DateTime DueDate { get; set; }

        public decimal NetAmount { get; set; }

        public decimal TaxAmount { get; set; }

        public string Note { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;//default

        public int ClientId { get; set; }
        public Client Client { get; set; }

        public int ProjectId { get; set; }
        public Project Project { get; set; }

        public Invoice()
        {
        }

        public Invoice(DateTime invoiceDate, DateTime dueDate, decimal netAmount, decimal taxAmount, string note, InvoiceStatus status, int clientId, int projectId)
        {
            InvoiceDate = invoiceDate;
            DueDate = dueDate;
            NetAmount = netAmount;
            TaxAmount = taxAmount;
            Note = note;
            Status = status;
            ClientId = clientId;
            ProjectId = projectId;
        }

        public Invoice(int id, DateTime invoiceDate, DateTime dueDate, decimal netAmount, decimal taxAmount, string note, InvoiceStatus status, int clientId, int projectId) 
            : this(invoiceDate, dueDate, netAmount, taxAmount, note, status, clientId, projectId)
        {
            Id = id;
        }
    }
}
