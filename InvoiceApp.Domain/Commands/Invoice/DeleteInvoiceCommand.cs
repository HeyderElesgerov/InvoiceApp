using InvoiceApp.Domain.Core.CommandBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Invoice
{
    public class DeleteInvoiceCommand : Command
    {
        public int Id;

        public DeleteInvoiceCommand(int id)
        {
            Id = id;
        }
    }
}
