using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.TimeShiftItemViewModels;

namespace WebClient.Models.ProjectViewModels
{
    public class ChartTimeShiftItemForProjectViewModel
    {
        public IEnumerable<TimeShiftItem> TimeShiftItems { get; set; }

        public decimal TimeShiftDuration { get; set; }

        public decimal TimeShiftDurationBillable { get; set; }

        public decimal TimeShiftDurationNotBillable { get; set; }

        private decimal HoursPerDay = 8;

        public string ProjectName { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Dictionary<DateTime, decimal> TimeStatistics { get; set; }

        public Dictionary<string, decimal> StatisticsByEmployee { get; set; }

        public List<ProjectStatisticsHelper> ProjectStatisticsHelpers { get; set; }

        public ChartTimeShiftItemForProjectViewModel()
        {

        }

        public ChartTimeShiftItemForProjectViewModel(TimeShiftReportByClientDTO timeShiftReportDTO,
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

            StatisticsByEmployee = new Dictionary<string, decimal>();
            foreach (Employee employee in employees)
            {
                decimal duration = TimeShiftItems.Where(x => x.EmployeeId == employee.Id).Sum(x => x.Duration);
                if (duration > 0)
                    StatisticsByEmployee.Add(employee.UserName, duration);
            }

            ProjectStatisticsHelpers = ChartDataBuilder.InitialazeTimeStatisticsByProject(TimeShiftItems,
                                                                                    project,
                                                                                    DateFrom,
                                                                                    DateTo);
        }
    }
}
