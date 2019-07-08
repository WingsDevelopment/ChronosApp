using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.TimeShiftItemViewModels
{
    public class ListOfTimeShiftItemsViewModel
    {
        public IEnumerable<TimeShiftItem> TimeShiftItems { get; set; }

        public List<SelectListItem> Projects { get; set; }

        public decimal TimeShiftDuration { get; set; }

        public decimal OffTimeDuration { get; set; }

        private decimal HoursPerDay { get; set; }

        public string UserName{ get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public ListOfTimeShiftItemsViewModel()
        {

        }

        public ListOfTimeShiftItemsViewModel(TimeShiftReportDTO timeShiftReportDTO,
                                             Employee employee)
        {
            TimeShiftItems = timeShiftReportDTO.TimeShiftItems;
            TimeShiftDuration = timeShiftReportDTO.TimeShiftDuration;
            OffTimeDuration = timeShiftReportDTO.OffTimeDuration;
            DateFrom = timeShiftReportDTO.DateFrom;
            DateTo = timeShiftReportDTO.DateTo;
            HoursPerDay = employee.HourPerDay;
            UserName = employee.UserName;

            Projects = new List<SelectListItem>();
            foreach (Project project in employee.PermitedProjects)
            {
                var item = new SelectListItem();
                item.Text = project.ProjectName;
                item.Value = project.Id.ToString();

                Projects.Add(item);
            }
        }

        public string GetTimeFromMinutes(decimal minutes)
        {
            if (minutes <= 0)
            {
                return "0d:0h:00m";
            }
            //days
            int days = (int)((minutes / 60) / HoursPerDay);
            //hours
            int t = (int)minutes;
            if (days > 0)
                t = (int)(minutes - (days * HoursPerDay * 60));
            int hours = (int)(t / 60);
            //mins
            int mins = (int)(minutes % 60);

            string time = string.Format($"{days}d:{hours}h:{mins}m");

            return time;
        }

        public string GetDaysFromMinutes(decimal minutes)
        {
            if (minutes <= 0)
            {
                return "None";
            }
            //days
            int days = (int)((minutes / 60) / HoursPerDay);

            string strDays = string.Empty;
            if (days == 1)
                strDays = "day";
            else
                strDays = "days";

            string time = string.Format($"{days} {strDays}");

            return time;
        }

        public decimal GetProgress()
        {
            int numberOfDays = NumberOfBusinessDays(DateFrom, DateTo);
            decimal totalMinutes = (numberOfDays * HoursPerDay * 60);
            decimal actualMinutes = TimeShiftDuration + OffTimeDuration;
            return (100 * actualMinutes / totalMinutes);
        }

        private int NumberOfBusinessDays(DateTime from, DateTime to)
        {
            int numberOfDays = 0;
            for (var day = from.Date; day.Date <= to.Date; day = day.AddDays(1))
            {
                if (day.DayOfWeek == DayOfWeek.Saturday ||
                    day.DayOfWeek == DayOfWeek.Sunday)
                    continue;
                numberOfDays++;
            }
            return numberOfDays;
        }
    }
}
