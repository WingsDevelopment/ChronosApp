using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ProjectViewModels
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ClientId { get; set; }

        [Required]
        public string ProjectName { get; set; }

        public List<SelectListItem> Clients { get; set; }

        public ProjectViewModel()
        {
        }

        public ProjectViewModel(IEnumerable<Client> clients)
        {
            Clients = new List<SelectListItem>();
            foreach (Client client in clients)
            {
                var item = new SelectListItem();
                item.Text = client.ClientName;
                item.Value = client.Id.ToString();

                Clients.Add(item);
            }
        }

        public ProjectViewModel(IEnumerable<Client> clients, int selectedId)
        {
            Clients = new List<SelectListItem>();
            foreach (Client client in clients)
            {
                var item = new SelectListItem();
                item.Text = client.ClientName;
                item.Value = client.Id.ToString();
                if (client.Id == selectedId)
                {
                    item.Selected = true;
                }

                Clients.Add(item);
            }
        }

        public ProjectViewModel(Project project, IEnumerable<Client> clients) : this(clients, project.ClientId)
        {
            Id = project.Id;
            ProjectName = project.ProjectName;
            ClientId = project.ClientId;
        }
    }
}
