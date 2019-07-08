using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface IClientRepository
    {
        void Update(Client client);
        Client FindById(int id);
        void Add(Client client);
        IEnumerable<Client> FindAll();
    }
}
