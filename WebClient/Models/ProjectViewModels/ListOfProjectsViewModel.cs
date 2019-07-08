using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ProjectViewModels
{
    public class ListOfProjectsViewModel
    {
        public IEnumerable<Project> Projects { get; set; }

        public ListOfProjectsViewModel()
        {

        }

        public ListOfProjectsViewModel(IEnumerable<Project> projects)
        {
            Projects = projects;
        }
    }
}
