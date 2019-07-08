using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class ExpenseTypeService : IExpenseTypeService
    {
        private IExpenseTypeRepository _expenseTypeRepository;

        public ExpenseTypeService(IExpenseTypeRepository expenseTypeRepository)
        {
            _expenseTypeRepository = expenseTypeRepository;
        }

        public void Add(ExpenseType expenseType)
        {
            try
            {
                _expenseTypeRepository.Add(expenseType);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(ExpenseType expenseType)
        {
            try
            {
                _expenseTypeRepository.Update(expenseType);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public ExpenseType FindById(int id)
        {
            try
            {
                return _expenseTypeRepository.FindById(id);
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
                ExpenseType expenseType = _expenseTypeRepository.FindById(id);

                expenseType.IsDeleted = true;

                _expenseTypeRepository.Update(expenseType);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<ExpenseType> FindAll()
        {
            try
            {
                return _expenseTypeRepository.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
