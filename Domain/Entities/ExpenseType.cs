using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class ExpenseType
    {
        public int Id { get; set; }

        public string ExpenseTypeName { get; set; }

        public string Discription { get; set; }

        public bool IsDeleted { get; set; }

        public ExpenseType()
        {

        }

        public ExpenseType(string expenseTypeName, string discription)
        {
            ExpenseTypeName = expenseTypeName;
            Discription = discription;
        }

    }
}
