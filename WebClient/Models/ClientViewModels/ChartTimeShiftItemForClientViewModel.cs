using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.TimeShiftItemViewModels;

namespace WebClient.Models.ClientViewModels
{
    public class ChartTimeShiftItemForClientViewModel
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

        public List<ProjectStatisticsHelper> ProjectStatisticsHelpers { get; set; }

        public ChartTimeShiftItemForClientViewModel()
        {

        }

        public ChartTimeShiftItemForClientViewModel(TimeShiftReportByClientDTO timeShiftReportDTO,
                                                    IEnumerable<Project> projects,
                                                    string clientName)
        {
            TimeShiftItems = timeShiftReportDTO.TimeShiftItems;

            TimeShiftDuration = timeShiftReportDTO.TimeShiftDuration;
            TimeShiftDurationBillable = timeShiftReportDTO.TimeShiftDurationBillable;
            TimeShiftDurationNotBillable = timeShiftReportDTO.TimeShiftDurationNotBillable;

            DateFrom = timeShiftReportDTO.DateFrom;
            DateTo = timeShiftReportDTO.DateTo;
            ClientName = clientName;

            ProjectStatistics = ChartDataBuilder.InitialazeProjectNameDurationDict(projects, TimeShiftItems);
            ProjectStatisticsHelpers = ChartDataBuilder.InitialazeTimeStatisticsByProject(TimeShiftItems,
                                                                                    projects,
                                                                                    DateFrom,
                                                                                    DateTo);
        }
    }
}
