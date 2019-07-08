using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class ClientService : IClientService
    {
        private IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public void Add(Client client)
        {
            _clientRepository.Add(client);
        }

        public void Update(Client client)
        {
            _clientRepository.Update(client);
        }

        public Client FindById(int id)
        {
            return _clientRepository.FindById(id);
        }

        public void Delete(int id)
        {
            try
            {
                Client client = _clientRepository.FindById(id);
                client.IsDeleted = true;
                _clientRepository.Update(client);
            }
            catch
            {

            }
        }

        public IEnumerable<Client> FindAll()
        {
            try
            {
                return _clientRepository.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
