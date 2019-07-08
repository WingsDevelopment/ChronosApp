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
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly string _connectionString = ConnectionStringContainer.ConnectionString;

        IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_connectionString);
            }
        }

        public void Add(Employee entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Insert(entity);
            }
        }

        public IEnumerable<Employee> FindAll()
        {
            using (IDbConnection db = Connection)
            {
                return db.GetAll<Employee>();
            }
        }

        public Employee FindById(int entityId)
        {
            using (IDbConnection db = Connection)
            {
                var getUserQuery =
                       $"SELECT *" +
                       $" FROM Employees " +
                       $" LEFT JOIN dbo.ProjectPermissions ON Employees.Id = ProjectPermissions.EmployeeId " +
                       $" LEFT JOIN dbo.Projects ON ProjectPermissions.ProjectId = Projects.Id " +
                       $" WHERE Employees.Id = @EmployeeId; ";

                Employee employee = db.Query<Employee, Project, Employee>(getUserQuery,
                    (Employee, Project) =>
                    {
                        Employee.PermitedProjects = Employee.PermitedProjects ?? new List<Project>();
                        Employee.PermitedProjects.Add(Project);
                        return Employee;
                    },
                    new { EmployeeId = entityId }).GroupBy(x => x.Id)
                    .Select(group =>
                    {
                        var combinedEmployee = group.First();
                        combinedEmployee.PermitedProjects = group.Select(CombinedEmployee => CombinedEmployee.PermitedProjects.SingleOrDefault()).ToList();
                        return combinedEmployee;
                    }).FirstOrDefault();

                if (employee == null)
                    return null;

                if (employee.PermitedProjects.Count == 1 && employee.PermitedProjects.First() == null)
                    employee.PermitedProjects = new List<Project>();

                return employee;
            }
        }

        public void Delete(Employee entity)
        {
            throw new NotImplementedException();
        }

        public void Update(Employee entity)
        {
            using (IDbConnection db = Connection)
            {
                db.Update<Employee>(entity);
            }
        }

        public Employee FindByUserName(string userName)
        {
            using (IDbConnection db = Connection)
            {
                var getUserQuery =
                       $"SELECT *" +
                       $" FROM Employees " +
                       $" LEFT JOIN dbo.ProjectPermissions ON Employees.Id = ProjectPermissions.EmployeeId " +
                       $" LEFT JOIN dbo.Projects ON ProjectPermissions.ProjectId = Projects.Id " +
                       $" WHERE Employees.UserName = @UserName; ";

                Employee employee = db.Query<Employee, Project, Employee>(getUserQuery,
                    (Employee, Project) =>
                    {
                        Employee.PermitedProjects = Employee.PermitedProjects ?? new List<Project>();
                        Employee.PermitedProjects.Add(Project);
                        return Employee;
                    },
                    new { UserName = userName }, splitOn: "Id").GroupBy(x => x.Id)
                    .Select(group =>
                    {
                        var combinedEmployee = group.First();
                        if (combinedEmployee == null)
                            return null;
                        combinedEmployee.PermitedProjects = group.Select(CombinedEmployee => CombinedEmployee.PermitedProjects.SingleOrDefault()).ToList();
                        return combinedEmployee;
                    }).FirstOrDefault();

                if (employee == null)
                    return null;

                if (employee.PermitedProjects.Count == 1 && employee.PermitedProjects.First() == null)
                    employee.PermitedProjects = new List<Project>();

                return employee;
            }
        }

        public void AddEmployeeToProject(int employeeId, int projectId)
        {
            using (IDbConnection db = Connection)
            {
                var sql =
                       $"INSERT INTO [dbo].[ProjectPermissions] " +
                       $"([ProjectId] ,[EmployeeId]) " +
                       $" VALUES (@ProjectId, @EmployeeId)";

                db.Execute(sql, new { EmployeeId = employeeId, ProjectId = projectId });
            }
        }

        public void RemoveEmployeeFromProject(int employeeId, int projectId)
        {
            using (IDbConnection db = Connection)
            {
                var sql =
                       $"DELETE FROM [dbo].[ProjectPermissions] " +
                       $"WHERE ProjectId = @ProjectId AND " +
                       $"EmployeeId = @EmployeeId";

                db.Execute(sql, new { EmployeeId = employeeId, ProjectId = projectId });
            }
        }

        public int FindCountByEmployeeIdProjectId(int employeeId, int projectId)
        {
            using (IDbConnection db = Connection)
            {
                var sql =
                       $"SELECT COUNT(ProjectId) FROM [dbo].[ProjectPermissions] " +
                       $"WHERE ProjectId = @ProjectId AND " +
                       $"EmployeeId = @EmployeeId";

                return db.Query<int>(sql, new { EmployeeId = employeeId, ProjectId = projectId }).Single();
            }
        }

        public bool ExistInAspTable(string userName)
        {
            using (IDbConnection db = Connection)
            {
                var sql =
                       $"SELECT UserName FROM [dbo].[AspNetUsers] " +
                       $"WHERE UserName = @UserName"; 

                var result = db.Query<string>(sql, new { UserName = userName }).Single();

                return !string.IsNullOrEmpty(result);
            }
        }
    }
}
