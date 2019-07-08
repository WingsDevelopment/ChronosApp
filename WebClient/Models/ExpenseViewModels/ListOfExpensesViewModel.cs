using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ExpenseViewModels
{
    public class ListOfExpensesViewModel
    {
        public IEnumerable<Expense> Expenses { get; set; }

        public List<SelectListItem> ExpenseTypes { get; set; }

        public ListOfExpensesViewModel()
        {

        }

        public ListOfExpensesViewModel(IEnumerable<Expense> expenses, IEnumerable<ExpenseType> expenseTypes)
        {
            Expenses = expenses;
            ExpenseTypes = new List<SelectListItem>();
            foreach (ExpenseType expenseType in expenseTypes)
            {
                var item = new SelectListItem();
                item.Text = expenseType.ExpenseTypeName;
                item.Value = expenseType.Id.ToString();

                ExpenseTypes.Add(item);
            }
        }
    }
}
