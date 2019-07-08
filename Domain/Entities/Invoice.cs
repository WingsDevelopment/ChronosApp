using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }

        public decimal Demand { get; set; }

        public decimal Paid { get; set; }

        public string Discription { get; set; }

        public DateTime CreationDate { get; set; }

        public bool IsDeleted { get; set; }

        public Invoice()
        {

        }

        public Invoice(decimal demand, decimal paid, string discription)
        {
            Demand = demand;
            Paid = paid;
            Discription = discription;
            CreationDate = DateTime.Now;
        }

        public Invoice(decimal demand, decimal paid, string discription, DateTime creationDate)
        {
            Demand = demand;
            Paid = paid;
            Discription = discription;
            CreationDate = creationDate;
        }
    }
}
