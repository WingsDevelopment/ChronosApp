using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ExpenseTypeViewModels
{
    public class ListOfExpenseTypesViewModel
    {
        public IEnumerable<ExpenseType> ExpenseTypes { get; set; }

        public ListOfExpenseTypesViewModel()
        {

        }

        public ListOfExpenseTypesViewModel(IEnumerable<ExpenseType> expenseTypes)
        {
            ExpenseTypes = expenseTypes;
        }
    }
}
