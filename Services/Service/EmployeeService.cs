using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class EmployeeService : IEmployeeService
    {
        private IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public void Add(Employee employee)
        {
            try
            {
                _employeeRepository.Add(employee);
                CheckIfEmployeeExistAndCreateOneIfNot(employee);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Employee employee)
        {
            try
            {
                _employeeRepository.Update(employee);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Employee FindById(int id)
        {
            try
            {
                return _employeeRepository.FindById(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(int id)
        {
            try
            {
                Employee employee = _employeeRepository.FindById(id);

                employee.IsDeleted = true;

                _employeeRepository.Update(employee);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Employee> FindAll()
        {
            try
            {
                return _employeeRepository.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Employee FindByUserName(string userName)
        {
            try
            {
                Employee employee = _employeeRepository.FindByUserName(userName);

                CheckIfEmployeeExistAndCreateOneIfNot(employee);

                return employee;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void CheckIfEmployeeExistAndCreateOneIfNot(Employee employee)
        {
            if (employee == null)
            {
                if (_employeeRepository.ExistInAspTable(employee.UserName))
                {
                    employee = new Employee(employee.UserName);

                    _employeeRepository.Add(employee);

                    throw new Exception("Something went wrong, please try again in a few moments");
                }
                else
                {
                    throw new NullReferenceException("Something went wrong, please try again in a few moments");
                }
            }
        }
        public void CheckIfEmployeeExistAndCreateOneIfNot(string userName)
        {
            Employee employee = _employeeRepository.FindByUserName(userName);

            if (employee == null)
            {
                if (_employeeRepository.ExistInAspTable(userName))
                {
                    employee = new Employee(userName);

                    _employeeRepository.Add(employee);

                    throw new Exception("Something went wrong, please try again in a few moments");
                }
                else
                {
                    throw new NullReferenceException("Something went wrong, please try again in a few moments");
                }
            }
        }

        public void AddEmployeeToProject(int employeeId, int projectId)
        {
            try
            {
                int count = _employeeRepository.FindCountByEmployeeIdProjectId(employeeId, projectId);

                if (count > 0)
                    throw new InvalidOperationException("Employee is already working on that project");

                _employeeRepository.AddEmployeeToProject(employeeId, projectId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void RemoveEmployeeFromProject(int employeeId, int projectId)
        {
            try
            {
                _employeeRepository.RemoveEmployeeFromProject(employeeId, projectId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
