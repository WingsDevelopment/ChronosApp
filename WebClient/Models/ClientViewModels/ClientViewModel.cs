using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebClient.Models.ClientViewModels
{
    public class ClientViewModel
    {

        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string ClientName { get; set; }

        public ClientViewModel()
        {

        }

        public ClientViewModel(Client client)
        {
            Id = client.Id;
            ClientName = client.ClientName;
        }
    }
}
