using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ProjectViewModels
{
    public class TimeShiftItemsByProjectViewModel
    {
        public List<SelectListItem> Projects { get; set; }

        [Required]
        public int ProjectId { get; set; }

        public string ChartType { get; set; }

        [Required]
        public DateTime DateFrom { get; set; }

        [Required]
        public DateTime DateTo { get; set; }

        public TimeShiftItemsByProjectViewModel()
        {

        }

        public TimeShiftItemsByProjectViewModel(IEnumerable<Project> projects)
        {
            Projects = new List<SelectListItem>();

            foreach (Project client in projects)
            {
                var item = new SelectListItem();
                item.Text = client.ProjectName;
                item.Value = client.Id.ToString();

                Projects.Add(item);
            }
        }
    }
}
