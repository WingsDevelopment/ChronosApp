using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface IInvoiceRepository
    {
        void Add(Invoice invoice);
        void Update(Invoice invoice);
        Invoice FindById(int id);
        IEnumerable<Invoice> FindAll();
    }
}
