using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.EmployeeViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
            
        public string UserName { get; set; }

        [Required]
        [Range(0, 24, ErrorMessage = "HourPerDay is out of range")]
        public decimal HourPerDay { get; set; }

        public int MainProjectId { get; set; }

        [Required]
        [Range(0, 500, ErrorMessage = "PriceLevel is out of range")]
        public int PriceLevel { get; set; }

        public int TeamdId { get; set; }

        public List<SelectListItem> PermitedProjectsSelect { get; set; }

        public List<SelectListItem> AllProjects { get; set; }

        public IEnumerable<Project> PermitedProjects { get; set; }

        public List<SelectListItem> Teams { get; set; }

        public EmployeeViewModel()
        {

        }

        public EmployeeViewModel(Employee employee)
        {
            Id = employee.Id;
            UserName = employee.UserName;
            HourPerDay = employee.HourPerDay;
            MainProjectId = employee.MainProjectId;
            PriceLevel = employee.PriceLevel;
            TeamdId = employee.TeamdId;
            PermitedProjects = employee.PermitedProjects;

            PermitedProjectsSelect = new List<SelectListItem>();
            foreach (Project project in employee.PermitedProjects)
            {
                var item = new SelectListItem();
                item.Text = project.ProjectName;
                item.Value = project.Id.ToString();
                if (project.Id == MainProjectId)
                {
                    item.Selected = true;
                }

                PermitedProjectsSelect.Add(item);
            }
        }

        public EmployeeViewModel(Employee employee, IEnumerable<Project> allProjects, IEnumerable<Team> teams) : this(employee)
        {
            AllProjects = new List<SelectListItem>();
            foreach (Project project in allProjects)
            {
                var item = new SelectListItem();
                item.Text = project.ProjectName;
                item.Value = project.Id.ToString();

                AllProjects.Add(item);
            }


            Teams = new List<SelectListItem>();
            foreach (Team team in teams)
            {
                var item = new SelectListItem();
                item.Text = team.TeamName;
                item.Value = team.Id.ToString();

                Teams.Add(item);
            }
        }
    }
}
