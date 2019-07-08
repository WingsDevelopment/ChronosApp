using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class TimeShiftReportDTO
    {
        public IEnumerable<TimeShiftItem> TimeShiftItems { get; set; }

        public decimal TimeShiftDuration { get; set; }

        public decimal TimeShiftDurationBillable { get; set; }

        public decimal TimeShiftDurationNotBillable { get; set; }

        public decimal OffTimeDuration { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public TimeShiftReportDTO()
        {
                
        }

        public TimeShiftReportDTO(IEnumerable<TimeShiftItem> timeShiftItems,
                                  decimal timeShiftDurationBillable,
                                  decimal timeShiftDurationNotBillable,
                                  decimal offTimeDuration,
                                  DateTime dateFrom,
                                  DateTime dateTo)
        {
            TimeShiftItems = timeShiftItems;
            TimeShiftDuration = timeShiftDurationBillable + timeShiftDurationNotBillable;
            TimeShiftDurationNotBillable = timeShiftDurationNotBillable;
            TimeShiftDurationBillable = timeShiftDurationBillable;
            OffTimeDuration = offTimeDuration;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}
