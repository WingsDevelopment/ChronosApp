using System.Collections.Generic;
using Domain.Entities;

namespace Services.Services
{
    public interface IProjectService
    {
        void Add(Project client);
        void Delete(int id);
        Project FindById(int id);
        void Update(Project client);
        IEnumerable<Project> FindAll();
        IEnumerable<Project> FindByClientId(int clientId);
    }
}