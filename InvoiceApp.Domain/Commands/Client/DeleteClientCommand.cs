using InvoiceApp.Domain.Core.CommandBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Client
{
    public class DeleteClientCommand : Command
    {
        public int ClientId;

        public DeleteClientCommand(int clientId)
        {
            ClientId = clientId;
        }
    }
}
