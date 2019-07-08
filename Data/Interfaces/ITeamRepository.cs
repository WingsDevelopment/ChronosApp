using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;

namespace Data.Interfaces
{
    public interface ITeamRepository
    {
        IEnumerable<Team> FindAll();
        void Delete(int id);
        Team FindById(int id);
        void Update(Team team);
        void Add(Team team);
    }
}
