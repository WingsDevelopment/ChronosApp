using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.InvoiceViewModels
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public decimal Demand { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public decimal Paid { get; set; }

        public string Discription { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsDeleted { get; set; }

        public InvoiceViewModel()
        {

        }

        public InvoiceViewModel(Invoice invoice)
        {
            Id = invoice.Id;
            Demand = invoice.Demand;
            Paid = invoice.Paid;
            Discription = invoice.Discription;
            CreationDate = invoice.CreationDate;
            IsDeleted = invoice.IsDeleted;
        }
    }
}
