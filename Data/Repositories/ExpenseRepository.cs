using Dapper;
using Dapper.Contrib.Extensions;
using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Chronos.Data.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly string _connectionString = ConnectionStringContainer.ConnectionString;

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Add(Expense entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert(entity);
            }
        }

        public IEnumerable<Expense> FindAll()
        {
            using (IDbConnection db = Connection)
            {
                var sql =
                       $"SELECT *" +
                       $" FROM Expenses " +
                       $" LEFT JOIN dbo.ExpenseTypes ON Expenses.ExpenseTypeId = ExpenseTypes.Id " +
                       $" WHERE Expenses.IsDeleted = 0; ";

                IEnumerable<Expense> expenses = db.Query<Expense, ExpenseType, Expense>(sql,
                    (Expense, ExpenseType) =>
                    {
                        Expense.ExpenseType = ExpenseType;
                        return Expense;
                    });

                return expenses;
            }
        }

        public Expense FindById(int entityId)
        {
            using (IDbConnection db = Connection)
            {
                return db.Get<Expense>(entityId);
            }
        }

        public void Delete(Expense entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Expense entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Update<Expense>(entity);
            }
        }
    }
}
