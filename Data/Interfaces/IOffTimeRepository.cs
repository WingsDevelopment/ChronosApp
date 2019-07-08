using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface IOffTimeRepository
    {
        void Add(OffTime offTime);
        void Update(OffTime offTime);
        OffTime FindById(int id);
        IEnumerable<OffTime> FindAll();
        IEnumerable<OffTime> FindAllByEmployeeIdForMonth(int id, int month, int year);
        IEnumerable<OffTime> FindAllByEmployeeIdForPeriod(int id, DateTime from, DateTime to);
        void AddMany(List<OffTime> offTimes);
        IEnumerable<OffTime> FindAllByEmployeeId(int id);
    }
}
