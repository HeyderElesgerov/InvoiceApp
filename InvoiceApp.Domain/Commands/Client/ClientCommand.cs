using InvoiceApp.Domain.Core.CommandBase;
using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Commands.Client
{
    public abstract class ClientCommand : Command
    {
        public Models.Client Client;

        public ClientCommand(string firstName, string lastName)
        {
            Client = new Models.Client(firstName, lastName);
        }

        public ClientCommand(int id, string firstName, string lastName)
        {
            Client = new Models.Client(id, firstName, lastName);
        }
    }
}
