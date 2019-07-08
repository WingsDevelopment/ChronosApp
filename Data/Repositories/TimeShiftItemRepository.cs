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
    public class TimeShiftItemRepository : ITimeShiftItemRepository
    {
        private readonly string _connectionString = ConnectionStringContainer.ConnectionString;

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Add(TimeShiftItem entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert<TimeShiftItem>(entity);
            }
        }

        public IEnumerable<TimeShiftItem> FindAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.GetAll<TimeShiftItem>();
            }
        }

        public TimeShiftItem FindById(int entityId)
        {
            using (IDbConnection db = Connection)
            {
                return db.Get<TimeShiftItem>(entityId);
            }
        }

        public void Delete(TimeShiftItem entity)
        {
            throw new NotImplementedException();
        }

        public void Update(TimeShiftItem entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Update<TimeShiftItem>(entity);
            }
        }

        public IEnumerable<TimeShiftItem> FindAllByEmployeeIdForMonth(int employeeId, int month, int year)
        {
            string sql = " SELECT * FROM TimeShiftItems " +
                         " INNER JOIN Projects ON Projects.Id = TimeShiftItems.ProjectId " +
                         " WHERE MONTH(TimeShiftItems.Date) = @Month " +
                         " AND YEAR(TimeShiftItems.Date) = @Year " +
                         " AND TimeShiftItems.IsDeleted = 0 " +
                         " AND EmployeeId = @EmployeeId";
            using (IDbConnection db = Connection)
            {
                var items = db.Query<TimeShiftItem, Project, TimeShiftItem>(sql,
                    (TimeShiftItem, Project) =>
                    {
                        TimeShiftItem.Project = Project;
                        return TimeShiftItem;
                    }, new
                    {
                        EmployeeId = employeeId,
                        Month = month,
                        Year = year
                    }, splitOn: "Id").OrderBy(x => x.Date).ToList();
                return items;
            }
        }

        public IEnumerable<TimeShiftItem> FindAllByEmployeeIdForPeriod(int employeeId, DateTime from, DateTime to)
        {
            string sql = " SELECT * FROM TimeShiftItems " +
                         " INNER JOIN Projects ON Projects.Id = TimeShiftItems.ProjectId " +
                         " WHERE TimeShiftItems.Date >= @From " +
                         " AND TimeShiftItems.Date <= @To " +
                         " AND TimeShiftItems.IsDeleted = 0 " +
                         " AND EmployeeId = @EmployeeId";
            using (IDbConnection db = Connection)
            {
                var items = db.Query<TimeShiftItem, Project, TimeShiftItem>(sql,
                    (TimeShiftItem, Project) =>
                    {
                        TimeShiftItem.Project = Project;
                        return TimeShiftItem;
                    }, new
                    {
                        EmployeeId = employeeId,
                        From = from,
                        To = to
                    }, splitOn: "Id").OrderBy(x => x.Date).ToList();
                return items;
            }
        }

        public IEnumerable<TimeShiftItem> FindAllByClientIdForPeriod(int clientId, DateTime dateFrom, DateTime dateTo)
        {
            string sql = " SELECT * FROM TimeShiftItems " +
                         " INNER JOIN Projects ON Projects.Id = TimeShiftItems.ProjectId " +
                         " WHERE TimeShiftItems.Date >= @From " +
                         " AND TimeShiftItems.Date <= @To " +
                         " AND TimeShiftItems.IsDeleted = 0" +
                         " AND Projects.ClientId = @ClientId ";

            using (IDbConnection db = Connection)
            {
                var items = db.Query<TimeShiftItem, Project, TimeShiftItem>(sql,
                    (TimeShiftItem, Project) =>
                    {
                        TimeShiftItem.Project = Project;
                        return TimeShiftItem;
                    }, new
                    {
                        ClientId = clientId,
                        From = dateFrom,
                        To = dateTo
                    }, splitOn: "Id").OrderBy(x => x.Date).ToList();
                return items;
            }
        }

        public IEnumerable<TimeShiftItem> FindAllByProjectIdForPeriod(int projectId, DateTime dateFrom, DateTime dateTo)
        {
            string sql = " SELECT * FROM TimeShiftItems " +
                         " INNER JOIN Projects ON Projects.Id = @ProjectId " +
                         " WHERE TimeShiftItems.Date >= @From " +
                         " AND TimeShiftItems.Date <= @To " +
                         " AND TimeShiftItems.IsDeleted = 0 " +
                         " AND TimeShiftItems.ProjectId = @ProjectId";

            using (IDbConnection db = Connection)
            {
                var items = db.Query<TimeShiftItem, Project, TimeShiftItem>(sql,
                    (TimeShiftItem, Project) =>
                    {
                        TimeShiftItem.Project = Project;
                        return TimeShiftItem;
                    }, new
                    {
                        ProjectId = projectId,
                        From = dateFrom,
                        To = dateTo
                    }, splitOn: "Id").OrderBy(x => x.Date).ToList();
                return items;
            }
        }
    }
}
