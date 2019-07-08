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
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly string _connectionString = ConnectionStringContainer.ConnectionString;

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Add(Invoice entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert(entity);
            }
        }

        public IEnumerable<Invoice> FindAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.GetAll<Invoice>().Where(x => !x.IsDeleted);
            }
        }

        public Invoice FindById(int entityId)
        {
            using (IDbConnection db = Connection)
            {
                return db.Get<Invoice>(entityId);
            }
        }

        public void Delete(Invoice entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Invoice entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Update<Invoice>(entity);
            }
        }
    }
}
