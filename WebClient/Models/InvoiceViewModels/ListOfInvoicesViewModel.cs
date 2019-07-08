using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.InvoiceViewModels
{
    public class ListOfInvoicesViewModel
    {
        public IEnumerable<Invoice> Invoices { get; set; }

        public ListOfInvoicesViewModel()
        {

        }

        public ListOfInvoicesViewModel(IEnumerable<Invoice> invoice)
        {
            Invoices = invoice;
        }
    }
}
