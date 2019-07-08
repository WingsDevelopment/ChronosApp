using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Helpers.Enum;

namespace WebClient.Models.EmployeeViewModels
{
    public class SelectListOfEmployeesViewModel
    {
        public int EmployeeId { get; set; }

        public List<SelectListItem> Employees { get; set; }

        public ViewType ViewType { get; set; }

        public ChartType ChartType { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public SelectListOfEmployeesViewModel()
        {
            ViewType = ViewType.Table;
            ChartType = ChartType.Bar;
        }

        public SelectListOfEmployeesViewModel(int employeeId) : this()
        {
            EmployeeId = employeeId;
        }
    }
}
