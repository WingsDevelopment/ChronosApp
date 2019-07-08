using Data.Interfaces;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class OffTimeService : IOffTimeService
    {
        private IOffTimeRepository _offTimeRepository;
        private ITimeShiftItemRepository _timeShiftItemRepository;

        public OffTimeService(IOffTimeRepository offTimeRepository,
                              ITimeShiftItemRepository timeShiftItemRepository)
        {
            _offTimeRepository = offTimeRepository;
            _timeShiftItemRepository = timeShiftItemRepository;
        }

        public void Add(OffTime offTime)
        {
            try
            {
                if (TimeShiftItemAlreadyExist(offTime.EmployeeId, offTime.Date, offTime.Date))
                {
                    throw new InvalidOperationException("You already worked on this date :/");
                }
                if (AnotherOffDayAlreadyExist(offTime))
                {
                    throw new InvalidOperationException("You already took day off for this date :/");
                }
                _offTimeRepository.Add(offTime);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(OffTime offTime)
        {
            try
            {
                if(TimeShiftItemAlreadyExist(offTime.EmployeeId, offTime.Date, offTime.Date))
                {
                    throw new InvalidOperationException("You already worked on this date :/");
                }
                if (AnotherOffDayAlreadyExist(offTime))
                {
                    throw new InvalidOperationException("You already took day off for this date :/");
                }
                _offTimeRepository.Update(offTime);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private bool AnotherOffDayAlreadyExist(OffTime offTime)
        {
            var items = _offTimeRepository.FindAllByEmployeeIdForPeriod(offTime.EmployeeId,
                                                                        offTime.Date,
                                                                        offTime.Date);
            if ((items.Count() == 1 && items.First().Id == offTime.Id))
                return false;

            if (items.Count() >= 1)
                return true;

            return false;
        }
        private bool OffDayAlreadyExist(int employeeId, DateTime from, DateTime to)
        {
            var items = _offTimeRepository.FindAllByEmployeeIdForPeriod(employeeId,
                                                                        from,
                                                                        to);
            if (items.Count() >= 1)
            {
                return true;
            }
            return false;
        }
        private bool TimeShiftItemAlreadyExist(int employeeId,
                                               DateTime from,
                                               DateTime to)
        {
            var items = _timeShiftItemRepository.FindAllByEmployeeIdForPeriod(employeeId,
                                                                              from,
                                                                              to);
            if (items.Count() >= 1)
            {
                return true;
            }
            return false;
        }

        public OffTime FindById(int id)
        {
            try
            {
                return _offTimeRepository.FindById(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(int id)
        {
            try
            {
                OffTime timeShiftItem = _offTimeRepository.FindById(id);

                timeShiftItem.IsDeleted = true;

                _offTimeRepository.Update(timeShiftItem);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<OffTime> FindAll()
        {
            try
            {
                return _offTimeRepository.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<OffTime> FindAllByEmployeeIdThisMonth(int id)
        {
            try
            {
                var dateTimeNow = DateTime.Now;
                return _offTimeRepository.FindAllByEmployeeIdForMonth(id, dateTimeNow.Month, dateTimeNow.Year);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<OffTime> FindAllByEmployeeIdForPeriod(int id, DateTime from, DateTime to)
        {
            try
            {
                return _offTimeRepository.FindAllByEmployeeIdForPeriod(id, from, to);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<OffTime> FindAllByEmployeeId(int id)
        {
            try
            {
                return _offTimeRepository.FindAllByEmployeeId(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void AddManyBetweenDates(int employeeId, OffTimeType offTimeType, string discription, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                List<OffTime> offTimes = new List<OffTime>();
                string errors = string.Empty;


                foreach (DateTime date in EachDay(dateFrom, dateTo))
                {
                    if (TimeShiftItemAlreadyExist(employeeId, date, date))
                    {
                        errors += $"You already worked on {date.ToShortDateString()} :/ " + Environment.NewLine;
                    }
                    if (OffDayAlreadyExist(employeeId, date, date))
                    {
                        errors += $"You already took day off on: {date.ToShortDateString()} :/ " + Environment.NewLine;
                    }
                    if ((date.DayOfWeek != DayOfWeek.Saturday) && (date.DayOfWeek != DayOfWeek.Sunday)){
                        
                        offTimes.Add(new OffTime(employeeId, date, offTimeType, discription));
                    }
                }
                if (string.IsNullOrEmpty(errors))
                    _offTimeRepository.AddMany(offTimes);
                else
                    throw new InvalidOperationException(errors);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private IEnumerable<DateTime> EachDay(DateTime from, DateTime to)
        {
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
                yield return day;
        }
    }
}
