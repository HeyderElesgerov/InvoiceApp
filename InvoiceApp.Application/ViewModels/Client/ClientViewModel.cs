using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Application.ViewModels.Client
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string FullName
        {
            get
            {
                return FirstName + " " + LastName;
            }
        }
    }
}
