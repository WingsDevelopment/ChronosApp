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
    public class ProjectRepository : IProjectRepository
    {
        private readonly string _connectionString = ConnectionStringContainer.ConnectionString;

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Add(Project entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert(entity);
            }
        }

        public IEnumerable<Project> FindAll()
        {

            string sql = " SELECT * FROM Projects " +
                         " INNER JOIN Clients ON Clients.Id = Projects.ClientId " +
                         " WHERE Projects.IsDeleted = 0 ";
            using (IDbConnection db = Connection)
            {
                var items = db.Query<Project, Client, Project>(sql,
                    (Project, Client) =>
                    {
                        Project.Client = Client;
                        return Project;
                    }, splitOn: "Id").ToList();
                return items;
            }
        }

        public Project FindById(int entityId)
        {
            using (IDbConnection db = Connection)
            {
                Project project = db.Get<Project>(entityId);

                if (project.IsDeleted)
                    return null;

                return project;
            }
        }

        public void Delete(int entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Project entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Update<Project>(entity);
            }
        }

        public IEnumerable<Project> FindByClientId(int clientId)
        {
            using (IDbConnection db = Connection)
            {
                return db.GetAll<Project>().Where(x => x.ClientId == clientId);
            }
        }
    }
}
