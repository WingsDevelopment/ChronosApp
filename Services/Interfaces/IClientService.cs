using System.Collections.Generic;
using Domain.Entities;

namespace Services.Services
{
    public interface IClientService
    {
        void Add(Client client);
        void Delete(int id);
        Client FindById(int id);
        void Update(Client client);
        IEnumerable<Client> FindAll();
    }
}