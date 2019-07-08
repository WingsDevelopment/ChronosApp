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
    public class ExpenseTypeRepository : IExpenseTypeRepository
    {
        private readonly string _connectionString = ConnectionStringContainer.ConnectionString;

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Add(ExpenseType entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert(entity);
            }
        }

        public IEnumerable<ExpenseType> FindAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.GetAll<ExpenseType>().Where(x => !x.IsDeleted);
            }
        }

        public ExpenseType FindById(int entityId)
        {
            using (IDbConnection db = Connection)
            {
                ExpenseType expenseType = db.Get<ExpenseType>(entityId);

                if (expenseType.IsDeleted)
                    return null;

                return expenseType;
            }
        }

        public void Delete(ExpenseType entity)
        {
            throw new NotImplementedException();
        }

        public void Update(ExpenseType entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Update<ExpenseType>(entity);
            }
        }
    }
}
