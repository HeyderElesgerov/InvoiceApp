using InvoiceApp.Domain.Core.Entity;
using System.Collections.Generic;

namespace InvoiceApp.Domain.Models
{
    public class Client : BaseEntity<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        
        public Client()
        {
        }

        public Client(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }

        public Client(int id, string firstName, string lastName) : this(firstName, lastName)
        {
            Id = id;
        }

        public void ChangeFirstName(string newFirstName)
        {
            FirstName = newFirstName;
        }

        public void ChangeLastName(string newLastName)
        {
            LastName = newLastName;
        }
    }
}
