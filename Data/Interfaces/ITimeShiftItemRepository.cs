using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface ITimeShiftItemRepository
    {
        IEnumerable<TimeShiftItem> FindAllByEmployeeIdForMonth(int id, int month, int year);
        IEnumerable<TimeShiftItem> FindAllByEmployeeIdForPeriod(int id, DateTime from, DateTime to);
        IEnumerable<TimeShiftItem> FindAll();
        void Update(TimeShiftItem timeShiftItem);
        TimeShiftItem FindById(int id);
        void Add(TimeShiftItem timeShiftItem);
        IEnumerable<TimeShiftItem> FindAllByClientIdForPeriod(int clientId, DateTime dateFrom, DateTime dateTo);
        IEnumerable<TimeShiftItem> FindAllByProjectIdForPeriod(int projectId, DateTime dateFrom, DateTime dateTo);
    }
}
