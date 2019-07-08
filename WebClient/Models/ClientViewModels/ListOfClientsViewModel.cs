using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ClientViewModels
{
    public class ListOfClientsViewModel
    {
        public IEnumerable<Client> Clients { get; set; }

        public ListOfClientsViewModel()
        {

        }

        public ListOfClientsViewModel(IEnumerable<Client> client)
        {
            Clients = client;
        }
    }
}
