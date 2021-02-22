using InvoiceApp.Domain.Core.Entity;
using System.Collections.Generic;

namespace InvoiceApp.Domain.Models
{
    public class Project : BaseEntity<int>
    {
        public string Name { get; set; }
        public ICollection<Invoice> Invoices { get; set; }

        public Project()
        {
        }

        public Project(string name)
        {
            Name = name;
        }

        public Project(int id, string name) : this(name)
        {
            Id = id;
        }

        public void ChangeName(string newName)
        {
            Name = newName;
        }
    }
}
