using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.TimeShiftItemViewModels;

namespace WebClient.Models.ClientViewModels
{
    public class ChartTimeShiftItemForClientPerEmployeeViewModel
    {
        public IEnumerable<TimeShiftItem> TimeShiftItems { get; set; }

        public decimal TimeShiftDuration { get; set; }

        public decimal TimeShiftDurationBillable { get; set; }

        public decimal TimeShiftDurationNotBillable { get; set; }

        private decimal HoursPerDay = 8;

        public string ClientName { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Dictionary<string, decimal> ProjectStatistics { get; set; }

        public Dictionary<DateTime, decimal> TimeStatistics { get; set; }

        public List<Dictionary<DateTime, decimal>> TimeStatisticsByProject { get; set; }

        public List<ProjectStatisticsPerEmployeeHelper> ProjectStatisticsPerEmployee { get; set; }

        public Dictionary<int, string> EmpIdEmpNameHelperDict { get; set; }

        public IEnumerable<DateTime> Days { get; set; }

        public ChartTimeShiftItemForClientPerEmployeeViewModel()
        {

        }

        public ChartTimeShiftItemForClientPerEmployeeViewModel(TimeShiftReportByClientDTO timeShiftReportDTO,
                                                               IEnumerable<Project> projects,
                                                               IEnumerable<Employee> employees,
                                                               string clientName)
        {
            TimeShiftItems = timeShiftReportDTO.TimeShiftItems;

            TimeShiftDuration = timeShiftReportDTO.TimeShiftDuration;
            TimeShiftDurationBillable = timeShiftReportDTO.TimeShiftDurationBillable;
            TimeShiftDurationNotBillable = timeShiftReportDTO.TimeShiftDurationNotBillable;

            DateFrom = timeShiftReportDTO.DateFrom;
            DateTo = timeShiftReportDTO.DateTo;
            ClientName = clientName;
            Days = TimeHelper.EachBusinessDay(DateFrom, DateTo);

            ProjectStatistics = ChartDataBuilder.InitialazeProjectNameDurationDict(projects, TimeShiftItems);

            var dto = ChartDataBuilder.InitialazeTimeStatisticsByProjectPerEmployee
                (TimeShiftItems,
                projects,
                employees,
                DateFrom,
                DateTo);

            ProjectStatisticsPerEmployee = dto.ProjectStatisticsPerEmployeeHelpers;
            EmpIdEmpNameHelperDict = dto.EmpIdEmpNameHelperDict;
        }
    }
}
