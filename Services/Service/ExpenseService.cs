using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class ExpenseService : IExpenseService
    {
        private IExpenseRepository _expenseRepository;

        public ExpenseService(IExpenseRepository expenseRepository)
        {
            _expenseRepository = expenseRepository;
        }

        public void Add(Expense expense)
        {
            try
            {
                _expenseRepository.Add(expense);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Expense expense)
        {
            try
            {
                _expenseRepository.Update(expense);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Expense FindById(int id)
        {
            try
            {
                return _expenseRepository.FindById(id);
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
                Expense expense = _expenseRepository.FindById(id);

                expense.IsDeleted = true;

                _expenseRepository.Update(expense);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Expense> FindAll()
        {
            try
            {
                return _expenseRepository.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
