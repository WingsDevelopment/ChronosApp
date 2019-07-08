using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class InvoiceService : IInvoiceService
    {
        private IInvoiceRepository _invoiceRepository;

        public InvoiceService(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public void Add(Invoice invoice)
        {
            try
            {
                _invoiceRepository.Add(invoice);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Invoice invoice)
        {
            try
            {
                _invoiceRepository.Update(invoice);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Invoice FindById(int id)
        {
            try
            {
                return _invoiceRepository.FindById(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(int id)
        {
            try
            {
                Invoice invoice = _invoiceRepository.FindById(id);

                invoice.IsDeleted = true;

                _invoiceRepository.Update(invoice);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Invoice> FindAll()
        {
            try
            {
                return _invoiceRepository.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
