using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface IProjectRepository
    {
        void Add(Project team);
        void Update(Project team);
        Project FindById(int id);
        void Delete(int id);
        IEnumerable<Project> FindAll();
        IEnumerable<Project> FindByClientId(int clientId);
    }
}
