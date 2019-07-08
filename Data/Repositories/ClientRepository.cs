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
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString = ConnectionStringContainer.ConnectionString;

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Add(Client entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert(entity);
            }
        }

        public IEnumerable<Client> FindAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.GetAll<Client>().Where(x => !x.IsDeleted);
            }
        }

        public Client FindById(int entityId)
        {
            using (IDbConnection db = Connection)
            {
                return db.Get<Client>(entityId);
            }
        }

        public void Delete(Client entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Client entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Update<Client>(entity);
            }
        }
        
    }
}
