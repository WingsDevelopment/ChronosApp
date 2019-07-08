using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.EmployeeViewModels
{
    public class ListOfEmployeesViewModel
    {
        public IEnumerable<Employee> Employees { get; set; }

        public ListOfEmployeesViewModel()
        {

        }

        public ListOfEmployeesViewModel(IEnumerable<Employee> employees)
        {
            Employees = employees;
        }
    }
}
