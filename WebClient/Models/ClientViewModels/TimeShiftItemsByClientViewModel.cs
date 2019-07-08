using Domain.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ClientViewModels
{
    public class TimeShiftItemsByClientViewModel
    {
        public List<SelectListItem> Clients { get; set; }

        public int ClientId { get; set; }

        public string ChartType { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public TimeShiftItemsByClientViewModel()
        {

        }

        public TimeShiftItemsByClientViewModel(IEnumerable<Client> clients)
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
    }
}
