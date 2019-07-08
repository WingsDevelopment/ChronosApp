using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ExpenseViewModels
{
    public class ExpenseViewModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public decimal Amount { get; set; }

        public List<SelectListItem> ExpenseTypes { get; set; }

        [Required]
        public int ExpenseTypeId { get; set; }

        [Required]
        public MethodOfPayment MethodOfPayment { get; set; }

        public bool IsDeleted { get; set; }

        public ExpenseViewModel()
        {

        }

        public ExpenseViewModel(Expense expense)
        {
            Id = expense.Id;
            Date = expense.Date;
            Amount = expense.Amount;
            ExpenseTypeId = expense.ExpenseTypeId;
            MethodOfPayment = expense.MethodOfPayment;
            IsDeleted = expense.IsDeleted;
        }

        public ExpenseViewModel(IEnumerable<ExpenseType> expenseTypes)
        {
            ExpenseTypes = new List<SelectListItem>();
            foreach (ExpenseType expenseType in expenseTypes)
            {
                var item = new SelectListItem();
                item.Text = expenseType.ExpenseTypeName;
                item.Value = expenseType.Id.ToString();

                ExpenseTypes.Add(item);
            }
        }

        public ExpenseViewModel(Expense expense, IEnumerable<ExpenseType> expenseTypes) : this (expense)
        {
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
