using System;
using System.Collections.Generic;
using Domain.Entities;

namespace Services.Services
{
    public interface ITimeShiftItemService
    {
        void Add(TimeShiftItem client);
        void Delete(int id);
        TimeShiftItem FindById(int id);
        void Update(TimeShiftItem client);
        IEnumerable<TimeShiftItem> FindAllByEmployeeIdThisMonth(int id);
        IEnumerable<TimeShiftItem> FindAllByEmployeeIdForPeriod(int id, DateTime from, DateTime to);
        IEnumerable<TimeShiftItem> FindAllByClientIdForPeriod(int clientId, DateTime dateFrom, DateTime dateTo);
        IEnumerable<TimeShiftItem> FindAllByProjectIdForPeriod(int projectId, DateTime dateFrom, DateTime dateTo);
    }
}