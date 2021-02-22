using System;
using System.Collections.Generic;
using System.Text;

namespace InvoiceApp.Domain.Core.Entity
{
    public abstract class BaseEntity<TKey> : IEntity
    {
        public TKey Id { get; set; }
    }
}
