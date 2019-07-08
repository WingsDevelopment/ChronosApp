using System;
using System.Collections.Generic;
using Domain.Entities;
using Domain.Enums;

namespace Services.Services
{
    public interface IOffTimeService
    {
        void Add(OffTime client);
        void Delete(int id);
        OffTime FindById(int id);
        void Update(OffTime client);
        IEnumerable<OffTime> FindAllByEmployeeIdThisMonth(int id);
        IEnumerable<OffTime> FindAllByEmployeeId(int id);
        IEnumerable<OffTime> FindAllByEmployeeIdForPeriod(int id, DateTime from, DateTime to);
        void AddManyBetweenDates(int employeeId, OffTimeType offTimeType, string discription, DateTime dateFrom, DateTime dateTo);
    }
}