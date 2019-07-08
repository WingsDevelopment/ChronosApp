using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers.DTOs;
using WebClient.Models.ClientViewModels;
using WebClient.Models.TimeShiftItemViewModels;

namespace WebClient.Helpers
{
    public static class ChartDataBuilder
    {
        public static Dictionary<DateTime, decimal> InitialazeDateDurationDict(IEnumerable<TimeShiftItem> timeShiftItems,
                                                                                     DateTime dateFrom,
                                                                                     DateTime dateTo)
        {
            var DateDurationDict = new Dictionary<DateTime, decimal>();

            decimal duration = 0;
            foreach (DateTime date in TimeHelper.EachBusinessDay(dateFrom, dateTo))
            {
                duration = timeShiftItems.Where(x => x.Date == date).Select(x => x.Duration).SingleOrDefault();
                if (DateDurationDict.ContainsKey(date))
                    DateDurationDict[date] += (duration / 60);
                else
                    DateDurationDict.Add(date, duration / 60);
            }
            return DateDurationDict;
        }

        public static Dictionary<string, decimal> InitialazeProjectNameDurationDict(IEnumerable<Project> projects,
                                                                                    IEnumerable<TimeShiftItem> timeShiftItems)
        {
            var projectNameDurationDict = new Dictionary<string, decimal>();

            decimal duration = 0;
            foreach (Project project in projects)
            {
                duration = timeShiftItems.Where(x => x.ProjectId == project.Id).Sum(s => s.Duration);
                if (duration > 0)
                    projectNameDurationDict.Add(project.ProjectName, duration);
            }

            return projectNameDurationDict;
        }


        public static List<ProjectStatisticsHelper> InitialazeTimeStatisticsByProject(IEnumerable<TimeShiftItem> timeShiftItems,
                                                       IEnumerable<Project> projects,
                                                       DateTime dateFrom,
                                                       DateTime dateTo)
        {
            decimal duration = 0;
            var ProjectStatisticsHelpers = new List<ProjectStatisticsHelper>();
            var timeStatistics = new Dictionary<DateTime, decimal>();
            List<IEnumerable<TimeShiftItem>> listOfSeparatedTimeShifts = new List<IEnumerable<TimeShiftItem>>();

            foreach (Project project in projects)
            {
                var items = timeShiftItems.Where(x => x.ProjectId == project.Id);
                if (items != null && (items.Count() > 0))
                    listOfSeparatedTimeShifts.Add(items);
            }

            foreach (IEnumerable<TimeShiftItem> listOfTimeShifts in listOfSeparatedTimeShifts)
            {
                timeStatistics = new Dictionary<DateTime, decimal>();
                foreach (DateTime date in TimeHelper.EachBusinessDay(dateFrom, dateTo))
                {
                    duration = listOfTimeShifts.Where(x => x.Date == date).Sum(x => x.Duration);
                    timeStatistics.Add(date, duration / 60);
                }
                ProjectStatisticsHelpers.Add(
                    new ProjectStatisticsHelper(listOfTimeShifts.First().Project?.ProjectName,
                                                timeStatistics));
            }
            return ProjectStatisticsHelpers;
        }
        public static List<ProjectStatisticsHelper> InitialazeTimeStatisticsByProject(IEnumerable<TimeShiftItem> timeShiftItems,
                                                       Project project,
                                                       DateTime dateFrom,
                                                       DateTime dateTo)
        {
            decimal duration = 0;
            var ProjectStatisticsHelpers = new List<ProjectStatisticsHelper>();
            var timeStatistics = new Dictionary<DateTime, decimal>();

            timeStatistics = new Dictionary<DateTime, decimal>();
            foreach (DateTime date in TimeHelper.EachBusinessDay(dateFrom, dateTo))
            {
                duration = timeShiftItems.Where(x => x.Date == date).Sum(x => x.Duration);
                timeStatistics.Add(date, duration / 60);
            }
            ProjectStatisticsHelpers.Add(
                new ProjectStatisticsHelper(project.ProjectName,
                                            timeStatistics));

            return ProjectStatisticsHelpers;
        }

        public static ProjectStatisticsPerEmployeeHelperEmpIdEmpNameHelperDictDTO InitialazeTimeStatisticsByProjectPerEmployee(
            IEnumerable<TimeShiftItem> timeShiftItems,
            IEnumerable<Project> projects,
            IEnumerable<Employee> employees,
            DateTime dateFrom,
            DateTime dateTo)
        {
            decimal duration = 0;
            var ProjectStatisticsperEmployee = new List<ProjectStatisticsPerEmployeeHelper>();
            var dateTimeEmpIdDurationDict = new Dictionary<DateTime, Dictionary<int, decimal>>();

            var projectIdEmpIdTSIListDictDTO = InitialazeProjectIdEmpIdTimeShiftItemListDictInDict(projects,
                                                                          timeShiftItems,
                                                                          employees);
            var projectIdEmpIdTimeShiftItemListDict = projectIdEmpIdTSIListDictDTO.ListOfTimeShiftsByProjectPerEmployee;

            foreach (var dictEmpIdShiftItems in projectIdEmpIdTimeShiftItemListDict)
            {
                dateTimeEmpIdDurationDict = new Dictionary<DateTime, Dictionary<int, decimal>>();
                foreach (DateTime date in TimeHelper.EachBusinessDay(dateFrom, dateTo))
                {
                    foreach (Employee emp in employees)
                    {
                        if (!dictEmpIdShiftItems.Value.ContainsKey(emp.Id))
                            continue;

                        duration = dictEmpIdShiftItems.Value[emp.Id]
                            .Where(x => x.Date == date)
                            .Sum(x => x.Duration);
                        if (!dateTimeEmpIdDurationDict.ContainsKey(date))
                            dateTimeEmpIdDurationDict.Add(date, new Dictionary<int, decimal>());

                        if (dateTimeEmpIdDurationDict[date].ContainsKey(emp.Id))
                            dateTimeEmpIdDurationDict[date][emp.Id] += (duration / 60);
                        else
                            dateTimeEmpIdDurationDict[date].Add(emp.Id, duration / 60);
                    }
                }
                ProjectStatisticsperEmployee.Add(
                    new ProjectStatisticsPerEmployeeHelper
                        (dictEmpIdShiftItems.Value.First().Value.First().Project.ProjectName,
                        dateTimeEmpIdDurationDict));
            }

            var dto = new ProjectStatisticsPerEmployeeHelperEmpIdEmpNameHelperDictDTO(
                projectIdEmpIdTSIListDictDTO.EmpIdEmpNameHelperDict,
                ProjectStatisticsperEmployee);
            return dto;
        }
        public static ProjectStatisticsPerEmployeeHelperEmpIdEmpNameHelperDictDTO InitialazeTimeStatisticsByProjectPerEmployee(
            IEnumerable<TimeShiftItem> timeShiftItems,
            Project project,
            IEnumerable<Employee> employees,
            DateTime dateFrom,
            DateTime dateTo)
        {
            decimal duration = 0;
            var ProjectStatisticsperEmployee = new List<ProjectStatisticsPerEmployeeHelper>();
            var dateTimeEmpIdDurationDict = new Dictionary<DateTime, Dictionary<int, decimal>>();
            var dictEmpIdShiftItems = new Dictionary<int, List<TimeShiftItem>>();
            var EmpIdEmpNameHelperDict = new Dictionary<int, string>();
            
            foreach(Employee emp in employees)
            {
                EmpIdEmpNameHelperDict[emp.Id] = emp.UserName;
                dictEmpIdShiftItems[emp.Id] = timeShiftItems.Where(x => x.EmployeeId == emp.Id).ToList();
            }

            dateTimeEmpIdDurationDict = new Dictionary<DateTime, Dictionary<int, decimal>>();
            foreach (DateTime date in TimeHelper.EachBusinessDay(dateFrom, dateTo))
            {
                foreach (Employee emp in employees)
                {
                    duration = dictEmpIdShiftItems[emp.Id]
                        .Where(x => x.Date == date)
                        .Sum(x => x.Duration);
                    if (!dateTimeEmpIdDurationDict.ContainsKey(date))
                        dateTimeEmpIdDurationDict.Add(date, new Dictionary<int, decimal>());

                    if (dateTimeEmpIdDurationDict[date].ContainsKey(emp.Id))
                        dateTimeEmpIdDurationDict[date][emp.Id] += (duration / 60);
                    else
                        dateTimeEmpIdDurationDict[date].Add(emp.Id, duration / 60);
                }
            }
            ProjectStatisticsperEmployee.Add(
                new ProjectStatisticsPerEmployeeHelper
                    (project.ProjectName,
                    dateTimeEmpIdDurationDict));
            

            var dto = new ProjectStatisticsPerEmployeeHelperEmpIdEmpNameHelperDictDTO(
                EmpIdEmpNameHelperDict,
                ProjectStatisticsperEmployee);
            return dto;
        }

        private static ProjectIdEmpIdListOfTimeShiftItemsDictEmpIdEmpNameDictDTO InitialazeProjectIdEmpIdTimeShiftItemListDictInDict
            (IEnumerable<Project> projects,
             IEnumerable<TimeShiftItem> timeShiftItems,
             IEnumerable<Employee> employees)
        {
            Dictionary<int, Dictionary<int, List<TimeShiftItem>>> listOfTimeShiftsByProjectPerEmployee
                   = new Dictionary<int, Dictionary<int, List<TimeShiftItem>>>();
            var empIdEmpNameHelperDict = new Dictionary<int, string>();

            foreach (Project project in projects)
            {
                var items = timeShiftItems.Where(x => x.ProjectId == project.Id);
                if (items != null && (items.Count() > 0))
                {
                    foreach (Employee employee in employees)
                    {
                        if (!empIdEmpNameHelperDict.ContainsKey(employee.Id))
                        {
                            empIdEmpNameHelperDict.Add(employee.Id, employee.UserName);
                        }
                        if (listOfTimeShiftsByProjectPerEmployee.ContainsKey(project.Id))
                        {
                            listOfTimeShiftsByProjectPerEmployee[project.Id]
                                .Add(employee.Id, items.Where(x => x.EmployeeId == employee.Id).ToList());
                        }
                        else
                        {
                            var dic = new Dictionary<int, List<TimeShiftItem>>();
                            dic.Add(employee.Id, items.Where(x => x.EmployeeId == employee.Id).ToList());
                            listOfTimeShiftsByProjectPerEmployee.Add(project.Id, dic);
                        }
                    }
                }
            }

            var dto = new ProjectIdEmpIdListOfTimeShiftItemsDictEmpIdEmpNameDictDTO(listOfTimeShiftsByProjectPerEmployee,
                empIdEmpNameHelperDict);
            return dto;
        }
    }
}
