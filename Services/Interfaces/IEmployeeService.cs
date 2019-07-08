using System.Collections.Generic;
using Domain.Entities;

namespace Services.Services
{
    public interface IEmployeeService
    {
        void Add(Employee client);
        void Delete(int id);
        Employee FindById(int id);
        void Update(Employee client);
        Employee FindByUserName(string userName);
        IEnumerable<Employee> FindAll();
        void AddEmployeeToProject(int employeeId, int projectId);
        void RemoveEmployeeFromProject(int employeeId, int projectId);
        void CheckIfEmployeeExistAndCreateOneIfNot(string email);
    }
}