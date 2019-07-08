using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.TeamViewModels
{
    public class TeamViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        public string TeamName { get; set; }

        public List<SelectListItem> Projects { get; set; }

        public TeamViewModel()
        {
        }

        public TeamViewModel(IEnumerable<Project> projects)
        {
            Projects = new List<SelectListItem>();
            foreach (Project project in projects)
            {
                var item = new SelectListItem();
                item.Text = project.ProjectName;
                item.Value = project.Id.ToString();

                Projects.Add(item);
            }
        }
        
        public TeamViewModel(IEnumerable<Project> projects, int selectedId)
        {
            Projects = new List<SelectListItem>();
            foreach (Project project in projects)
            {
                var item = new SelectListItem();
                item.Text = project.ProjectName;
                item.Value = project.Id.ToString();
                if (project.Id == selectedId)
                {
                    item.Selected = true;
                }

                Projects.Add(item);
            }
        }

        public TeamViewModel(Team team, IEnumerable<Project> projects) : this(projects, team.ProjectId)
        {
            Id = team.Id;
            TeamName = team.TeamName;
            ProjectId = team.ProjectId;
        }
    }
}
