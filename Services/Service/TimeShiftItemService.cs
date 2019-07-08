using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services.Services
{
    public class TimeShiftItemService : ITimeShiftItemService
    {
        private ITimeShiftItemRepository _timeShiftItemRepository;
        private IOffTimeRepository _offTimeRepository;

        public TimeShiftItemService(ITimeShiftItemRepository timeShiftItemRepository, IOffTimeRepository offTimeRepository)
        {
            _timeShiftItemRepository = timeShiftItemRepository;
            _offTimeRepository = offTimeRepository;
        }

        public void Add(TimeShiftItem timeShiftItem)
        {
            try
            {
                IEnumerable<TimeShiftItem> items = 
                    _timeShiftItemRepository.FindAllByEmployeeIdForPeriod(timeShiftItem.EmployeeId,
                                                                          timeShiftItem.Date, 
                                                                          timeShiftItem.Date);

                if (items.Sum(x => x.Duration) + timeShiftItem.Duration > 1440)
                {
                    throw new InvalidOperationException("You can't work more then 24h per day :/");
                }

                if (OffDayAlreadyExist(timeShiftItem.EmployeeId, timeShiftItem.Date))
                {
                    throw new InvalidOperationException("You took day off for this date :/");
                }

                _timeShiftItemRepository.Add(timeShiftItem);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(TimeShiftItem timeShiftItem)
        {
            try
            {
                IEnumerable<TimeShiftItem> items =
                    _timeShiftItemRepository.FindAllByEmployeeIdForPeriod(timeShiftItem.EmployeeId,
                                                                          timeShiftItem.Date,
                                                                          timeShiftItem.Date);

                var sumDuration = items.Where(x => x.Id != timeShiftItem.Id).Sum(x => x.Duration);

                if (sumDuration + timeShiftItem.Duration > 1440)
                {
                    throw new InvalidOperationException("You can't work more then 24h per day :/");
                }
                
                if (OffDayAlreadyExist(timeShiftItem.EmployeeId, timeShiftItem.Date))
                {
                    throw new InvalidOperationException("You took day off for this date :/");
                }

                _timeShiftItemRepository.Update(timeShiftItem);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private bool OffDayAlreadyExist(int employeeId, DateTime date)
        {
            var items = _offTimeRepository.FindAllByEmployeeIdForPeriod(employeeId,
                                                                        date,
                                                                        date);
            if (items.Count() >= 1)
            {
                return true;
            }
            return false;
        }

        public TimeShiftItem FindById(int id)
        {
            try
            {
                return _timeShiftItemRepository.FindById(id);
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
                TimeShiftItem timeShiftItem = _timeShiftItemRepository.FindById(id);

                timeShiftItem.IsDeleted = true;

                _timeShiftItemRepository.Update(timeShiftItem);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<TimeShiftItem> FindAll()
        {
            try
            {
                return _timeShiftItemRepository.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<TimeShiftItem> FindAllByEmployeeIdThisMonth(int id)
        {
            try
            {
                var dateTimeNow = DateTime.Now;
                return _timeShiftItemRepository.FindAllByEmployeeIdForMonth(id, dateTimeNow.Month, dateTimeNow.Year);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<TimeShiftItem> FindAllByEmployeeIdForPeriod(int id, DateTime from, DateTime to)
        {
            try
            {
                return _timeShiftItemRepository.FindAllByEmployeeIdForPeriod(id, from, to);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<TimeShiftItem> FindAllByClientIdForPeriod(int clientId, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return _timeShiftItemRepository.FindAllByClientIdForPeriod(clientId, dateFrom, dateTo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<TimeShiftItem> FindAllByProjectIdForPeriod(int projectId, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                return _timeShiftItemRepository.FindAllByProjectIdForPeriod(projectId, dateFrom, dateTo);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
