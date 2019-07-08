using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.DTOs
{
    public class TimeShiftReportByClientDTO
    {
        public IEnumerable<TimeShiftItem> TimeShiftItems { get; set; }

        public decimal TimeShiftDuration { get; set; }

        public decimal TimeShiftDurationBillable { get; set; }

        public decimal TimeShiftDurationNotBillable { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public TimeShiftReportByClientDTO()
        {

        }

        public TimeShiftReportByClientDTO(IEnumerable<TimeShiftItem> timeShiftItems,
                                  decimal timeShiftDurationBillable,
                                  decimal timeShiftDurationNotBillable,
                                  DateTime dateFrom,
                                  DateTime dateTo)
        {
            TimeShiftItems = timeShiftItems;
            TimeShiftDuration = timeShiftDurationBillable + timeShiftDurationNotBillable;
            TimeShiftDurationNotBillable = timeShiftDurationNotBillable;
            TimeShiftDurationBillable = timeShiftDurationBillable;
            DateFrom = dateFrom;
            DateTo = dateTo;
        }
    }
}
