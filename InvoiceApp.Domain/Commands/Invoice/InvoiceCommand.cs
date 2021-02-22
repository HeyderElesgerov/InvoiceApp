using InvoiceApp.Domain.Core.CommandBase;
using InvoiceApp.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Invoice
{
    public abstract class InvoiceCommand : Command
    {
        public Models.Invoice Invoice;

        public InvoiceCommand(DateTime invoiceDate, DateTime dueDate, decimal netAmount, decimal taxAmount, string note, InvoiceStatus status, int clientId, int projectId)
        {
            Invoice = new Models.Invoice(invoiceDate, dueDate, netAmount, taxAmount, note, status, clientId, projectId);
        }

        public InvoiceCommand(int id, DateTime invoiceDate, DateTime dueDate, decimal netAmount, decimal taxAmount, string note, InvoiceStatus status, int clientId, int projectId)
        {
            Invoice = new Models.Invoice(id, invoiceDate, dueDate, netAmount, taxAmount, note, status, clientId, projectId);
        }
    }
}
