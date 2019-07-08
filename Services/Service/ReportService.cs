using Chronos.Services.Interfaces;
using Domain.DTOs;
using Domain.Entities;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Chronos.Services.Service
{
    public class ReportService : IReportService
    {
        ITimeShiftItemService _timeShiftItemService;
        IOffTimeService _offTimeService;

        public ReportService(ITimeShiftItemService timeShiftItemService, IOffTimeService offTimeService)
        {
            _timeShiftItemService = timeShiftItemService;
            _offTimeService = offTimeService;
        }

        public TimeShiftReportDTO GetTimeShiftReport(Employee employee,
                                                     IEnumerable<TimeShiftItem> timeShiftItems,
                                                     DateTime from,
                                                     DateTime to)
        {
            IEnumerable<OffTime> offTimes = 
                _offTimeService.FindAllByEmployeeIdForPeriod(employee.Id, from, to);

            TimeShiftReportDTO dto =
                new TimeShiftReportDTO(timeShiftItems,
                                       timeShiftItems.Where(x => x.IsBillable).Sum(x => x.Duration),
                                       timeShiftItems.Where(x => !x.IsBillable).Sum(x => x.Duration),
                                       offTimes.Count() * employee.HourPerDay * 60,
                                       from,
                                       to);

            return dto;
        }
        public TimeShiftReportDTO GetTimeShiftReportThisMonth(Employee employee,
                                                              IEnumerable<TimeShiftItem> timeShiftItems)
        {
            IEnumerable<OffTime> offTimes =
                _offTimeService.FindAllByEmployeeIdThisMonth(employee.Id);

            var date = DateTime.Now;
            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);

            TimeShiftReportDTO dto =
                new TimeShiftReportDTO(timeShiftItems,
                                       timeShiftItems.Where(x => x.IsBillable).Sum(x => x.Duration),
                                       timeShiftItems.Where(x => !x.IsBillable).Sum(x => x.Duration),
                                       offTimes.Count() * employee.HourPerDay * 60,
                                       firstDayOfMonth,
                                       lastDayOfMonth);

            return dto;
        }

        public TimeShiftReportByClientDTO GetTimeShiftReportByClient(int clientId,
                                                                     IEnumerable<TimeShiftItem> timeShiftItems,
                                                                     DateTime from,
                                                                     DateTime to)
        {
            TimeShiftReportByClientDTO dto =
                new TimeShiftReportByClientDTO(timeShiftItems,
                                               timeShiftItems.Where(x => x.IsBillable).Sum(x => x.Duration),
                                               timeShiftItems.Where(x => !x.IsBillable).Sum(x => x.Duration),
                                               from,
                                               to);

            return dto;
        }
    }
}
