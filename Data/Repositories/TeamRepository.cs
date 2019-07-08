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
    public class TeamRepository : ITeamRepository
    {
        private readonly string _connectionString = ConnectionStringContainer.ConnectionString;

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Add(Team entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert(entity);
            }
        }

        public IEnumerable<Team> FindAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.GetAll<Team>().Where(x => !x.IsDeleted);
            }
        }

        public Team FindById(int entityId)
        {
            using (IDbConnection db = Connection)
            {
                Team team = db.Get<Team>(entityId);

                if (team.IsDeleted)
                    return null;

                return team;
            }
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public void Update(Team entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Update<Team>(entity);
            }
        }
    }
}
