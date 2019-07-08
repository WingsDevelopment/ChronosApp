using Domain.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Chronos.Services.Interfaces
{
    public interface IReportService
    {
        TimeShiftReportDTO GetTimeShiftReport(Employee employee,
                                              IEnumerable<TimeShiftItem> timeShiftItems,
                                              DateTime from,
                                              DateTime to);

        TimeShiftReportDTO GetTimeShiftReportThisMonth(Employee employee,
                                                       IEnumerable<TimeShiftItem> timeShiftItems);
    }
}
