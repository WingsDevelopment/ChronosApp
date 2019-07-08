using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface IExpenseRepository
    {
        void Add(Expense expense);
        void Update(Expense expense);
        Expense FindById(int id);
        IEnumerable<Expense> FindAll();
    }
}
