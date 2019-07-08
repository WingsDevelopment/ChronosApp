using System.Collections.Generic;
using Domain.Entities;

namespace Services.Services
{
    public interface IInvoiceService
    {
        void Add(Invoice invoice);
        void Delete(int id);
        Invoice FindById(int id);
        void Update(Invoice invoice);
        IEnumerable<Invoice> FindAll();
    }
}