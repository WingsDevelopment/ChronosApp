using System.Collections.Generic;
using Domain.Entities;

namespace Services.Services
{
    public interface IExpenseTypeService
    {
        void Add(ExpenseType client);
        void Delete(int id);
        ExpenseType FindById(int id);
        void Update(ExpenseType client);
        IEnumerable<ExpenseType> FindAll();
    }
}