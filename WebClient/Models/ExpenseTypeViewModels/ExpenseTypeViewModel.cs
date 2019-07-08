using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ExpenseTypeViewModels
{
    public class ExpenseTypeViewModel
    {
        public int Id { get; set; }

        [Required]
        public string ExpenseTypeName { get; set; }

        public string Discription { get; set; }

        public ExpenseTypeViewModel()
        {

        }

        public ExpenseTypeViewModel(ExpenseType expenseType)
        {
            Id = expenseType.Id;
            ExpenseTypeName = expenseType.ExpenseTypeName;
            Discription = expenseType.Discription;
        }
    }
}
