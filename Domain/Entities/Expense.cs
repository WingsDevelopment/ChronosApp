using Dapper.Contrib.Extensions;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Expense
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal Amount { get; set; }

        public int ExpenseTypeId { get; set; }

        [Computed]
        public ExpenseType ExpenseType { get; set; }

        public MethodOfPayment MethodOfPayment { get; set; }

        public bool IsDeleted { get; set; }

        public Expense()
        {

        }

        public Expense(DateTime date, decimal amount, int expenseTypeId, MethodOfPayment methodOfPayment)
        {
            Date = date;
            Amount = amount;
            ExpenseTypeId = expenseTypeId;
            MethodOfPayment = methodOfPayment;
        }
    }
}
