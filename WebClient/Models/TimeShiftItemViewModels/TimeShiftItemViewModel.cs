using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.TimeShiftItemViewModels
{
    public class TimeShiftItemViewModel
    {
        public int Id { get; set; }

        public int EmployeeId { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue)]
        public int Duration { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string Discription { get; set; }

        public decimal SuggestedMinutes { get; set; }

        public bool IsBillable { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public List<SelectListItem> Projects { get; set; }

        public TimeShiftItemViewModel()
        {

        }

        public TimeShiftItemViewModel(TimeShiftItem timeShiftItem, IEnumerable<Project> projects)
        {
            Id = timeShiftItem.Id;
            IsBillable = timeShiftItem.IsBillable;
            Duration = timeShiftItem.Duration;
            Date = timeShiftItem.Date;
            Discription = timeShiftItem.Discription;
            ProjectId = timeShiftItem.ProjectId;
            EmployeeId = timeShiftItem.EmployeeId;

            Projects = new List<SelectListItem>();
            foreach (Project project in projects)
            {
                var item = new SelectListItem();
                item.Text = project.ProjectName;
                item.Value = project.Id.ToString();
                if (project.Id == timeShiftItem.ProjectId)
                {
                    item.Selected = true;
                }

                Projects.Add(item);
            }
        }

        public TimeShiftItemViewModel(Employee employee)
        {
            Projects = new List<SelectListItem>();
            foreach (Project project in employee.PermitedProjects)
            {
                var item = new SelectListItem();
                item.Text = project.ProjectName;
                item.Value = project.Id.ToString();
                if (project.Id == employee.MainProjectId)
                {
                    item.Selected = true;
                }

                Projects.Add(item);
            }
            SuggestedMinutes = employee.HourPerDay * 60;
            Date = DateTime.Now;
        }
    }
}
