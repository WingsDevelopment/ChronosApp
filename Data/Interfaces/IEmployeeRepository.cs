using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface IEmployeeRepository
    {
        void Add(Employee employee);
        void Update(Employee employee);
        Employee FindById(int id);
        Employee FindByUserName(string userName);
        IEnumerable<Employee> FindAll();
        void AddEmployeeToProject(int employeeId, int projectId);
        void RemoveEmployeeFromProject(int employeeId, int projectId);
        int FindCountByEmployeeIdProjectId(int employeeId, int projectId);
        bool ExistInAspTable(string userName);
    }
}
