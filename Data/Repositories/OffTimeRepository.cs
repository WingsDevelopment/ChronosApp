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
    public class OffTimeRepository : IOffTimeRepository
    {
        private readonly string _connectionString = ConnectionStringContainer.ConnectionString;

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Add(OffTime entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert(entity);
            }
        }

        public void AddMany(List<OffTime> offTimes)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert(offTimes);
            }
        }

        public IEnumerable<OffTime> FindAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.GetAll<OffTime>();
            }
        }

        public OffTime FindById(int entityId)
        {
            using (IDbConnection db = Connection)
            {
                return db.Get<OffTime>(entityId);
            }
        }

        public void Delete(OffTime entity)
        {
            throw new NotImplementedException();
        }

        public void Update(OffTime entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Update<OffTime>(entity);
            }
        }

        public IEnumerable<OffTime> FindAllByEmployeeIdForMonth(int id, int month, int year)
        {
            string sql = " SELECT * FROM OffTimes " +
                         " WHERE MONTH(OffTimes.Date) = @Month " +
                         " AND YEAR(OffTimes.Date) = @Year " +
                         " AND OffTimes.IsDeleted = 0" +
                         " AND OffTimes.EmployeeId = @EmployeeId";
            using (IDbConnection db = Connection)
            {
                var items = db.Query<OffTime>(sql, new
                    {
                        EmployeeId = id,
                        Month = month,
                        Year = year
                    }).OrderBy(x => x.Date).ToList();
                return items;
            }
        }

        public IEnumerable<OffTime> FindAllByEmployeeIdForPeriod(int id, DateTime from, DateTime to)
        {
            string sql = " SELECT * FROM OffTimes " +
                         " WHERE OffTimes.Date >= @From " +
                         " AND OffTimes.Date <= @To " +
                         " AND OffTimes.IsDeleted = 0 " +
                         " AND OffTimes.EmployeeId = @EmployeeId";

            using (IDbConnection db = Connection)
            {
                var items = db.Query<OffTime>(sql, new
                    {
                        EmployeeId = id,
                        From = from,
                        To = to
                    }).OrderBy(x => x.Date).ToList();
                return items;
            }
        }

        public IEnumerable<OffTime> FindAllByEmployeeId(int id)
        {
            string sql = " SELECT * FROM OffTimes " +
                         " WHERE OffTimes.EmployeeId = @EmployeeId AND" +
                         " IsDeleted = 0 ";

            using (IDbConnection db = Connection)
            {
                var items = db.Query<OffTime>(sql, new
                {
                    EmployeeId = id,
                }).OrderBy(x => x.Date).ToList();
                return items;
            }
        }
    }
}
