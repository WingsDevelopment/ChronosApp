using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.ClientViewModels;

namespace WebClient.Models.ProjectViewModels
{
    public class ChartTimeShiftItemForProjectPerEmployeeViewModel
    {
        public IEnumerable<TimeShiftItem> TimeShiftItems { get; set; }

        public decimal TimeShiftDuration { get; set; }

        public decimal TimeShiftDurationBillable { get; set; }

        public decimal TimeShiftDurationNotBillable { get; set; }

        private decimal HoursPerDay = 8;

        public string ProjectName { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Dictionary<string, decimal> StatisticsByEmployee { get; set; }

        public List<ProjectStatisticsPerEmployeeHelper> ProjectStatisticsPerEmployee { get; set; }

        public Dictionary<int, string> EmpIdEmpNameHelperDict { get; set; }

        public IEnumerable<DateTime> Days { get; set; }

        public ChartTimeShiftItemForProjectPerEmployeeViewModel()
        {

        }

        public ChartTimeShiftItemForProjectPerEmployeeViewModel(TimeShiftReportByClientDTO timeShiftReportDTO,
                                                               Project project,
                                                               IEnumerable<Employee> employees)
        {
            TimeShiftItems = timeShiftReportDTO.TimeShiftItems;

            TimeShiftDuration = timeShiftReportDTO.TimeShiftDuration;
            TimeShiftDurationBillable = timeShiftReportDTO.TimeShiftDurationBillable;
            TimeShiftDurationNotBillable = timeShiftReportDTO.TimeShiftDurationNotBillable;

            DateFrom = timeShiftReportDTO.DateFrom;
            DateTo = timeShiftReportDTO.DateTo;
            ProjectName = project.ProjectName;
            Days = TimeHelper.EachBusinessDay(DateFrom, DateTo);

            var dto = ChartDataBuilder.InitialazeTimeStatisticsByProjectPerEmployee
                (TimeShiftItems,
                project,
                employees,
                DateFrom,
                DateTo);

            ProjectStatisticsPerEmployee = dto.ProjectStatisticsPerEmployeeHelpers;
            EmpIdEmpNameHelperDict = dto.EmpIdEmpNameHelperDict;

            StatisticsByEmployee = new Dictionary<string, decimal>();
            foreach (Employee employee in employees)
            {
                decimal duration = TimeShiftItems.Where(x => x.EmployeeId == employee.Id).Sum(x => x.Duration);
                if (duration > 0)
                    StatisticsByEmployee.Add(employee.UserName, duration);
            }
        }
    }
}
