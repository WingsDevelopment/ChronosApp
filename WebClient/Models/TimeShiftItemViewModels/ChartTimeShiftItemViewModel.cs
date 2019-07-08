using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Helpers.Enum;

namespace WebClient.Models.TimeShiftItemViewModels
{
    public class ChartTimeShiftItemViewModel
    {
        public IEnumerable<TimeShiftItem> TimeShiftItems { get; set; }

        public List<SelectListItem> Projects { get; set; }

        public decimal TimeShiftDuration { get; set; }

        public decimal TimeShiftDurationBillable { get; set; }

        public decimal TimeShiftDurationNotBillable { get; set; }

        public decimal OffTimeDuration { get; set; }

        public ChartType ChartType { get; set; }

        private decimal HoursPerDay { get; set; }

        public string UserName { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public Dictionary<string, decimal> ProjectStatistics { get; set; }

        public Dictionary<DateTime, decimal> TimeStatistics { get; set; }

        public List<Dictionary<DateTime, decimal>> TimeStatisticsByProject { get; set; }

        public List<ProjectStatisticsHelper> ProjectStatisticsHelpers { get; set; }

        public ChartTimeShiftItemViewModel()
        {

        }

        public ChartTimeShiftItemViewModel(TimeShiftReportDTO timeShiftReportDTO,
                                           Employee employee,
                                           IEnumerable<Project> projects,
                                           ChartType chartType)
        {
            TimeShiftItems = timeShiftReportDTO.TimeShiftItems;

            TimeShiftDuration = timeShiftReportDTO.TimeShiftDuration;
            TimeShiftDurationBillable = timeShiftReportDTO.TimeShiftDurationBillable;
            TimeShiftDurationNotBillable = timeShiftReportDTO.TimeShiftDurationNotBillable;
            OffTimeDuration = timeShiftReportDTO.OffTimeDuration;

            DateFrom = timeShiftReportDTO.DateFrom;
            DateTo = timeShiftReportDTO.DateTo;
            HoursPerDay = employee.HourPerDay;
            UserName = employee.UserName;
            ChartType = chartType;

            ProjectStatistics = ChartDataBuilder.InitialazeProjectNameDurationDict(projects, TimeShiftItems);
            ProjectStatisticsHelpers = ChartDataBuilder.InitialazeTimeStatisticsByProject(TimeShiftItems,
                                                                                    projects,
                                                                                    DateFrom,
                                                                                    DateTo);

            Projects = new List<SelectListItem>();
            foreach (Project project in employee.PermitedProjects)
            {
                var item = new SelectListItem();
                item.Text = project.ProjectName;
                item.Value = project.Id.ToString();

                Projects.Add(item);
            }
        }
    }
}
