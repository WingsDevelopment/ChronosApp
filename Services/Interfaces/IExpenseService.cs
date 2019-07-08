using System.Collections.Generic;
using Domain.Entities;

namespace Services.Services
{
    public interface IExpenseService
    {
        void Add(Expense client);
        void Delete(int id);
        Expense FindById(int id);
        void Update(Expense client);
        IEnumerable<Expense> FindAll();
    }
}