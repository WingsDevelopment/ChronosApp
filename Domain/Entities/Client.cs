using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }

        public string ClientName { get; set; }

        public bool IsDeleted { get; set; }

        public Client()
        {

        }

        public Client(string clientName)
        {
            ClientName = clientName;
        }

    }
}
