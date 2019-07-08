using System.Collections.Generic;
using Domain.Entities;

namespace Services.Services
{
    public interface ITeamService
    {
        void Add(Team client);
        void Delete(int id);
        Team FindById(int id);
        void Update(Team client);
        IEnumerable<Team> FindAll();
    }
}