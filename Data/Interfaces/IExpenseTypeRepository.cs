using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface IExpenseTypeRepository
    {
        void Add(ExpenseType expenseType);
        void Update(ExpenseType expenseType);
        ExpenseType FindById(int id);
        IEnumerable<ExpenseType> FindAll();
    }
}
