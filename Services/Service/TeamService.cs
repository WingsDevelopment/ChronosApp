using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class TeamService : ITeamService
    {
        private ITeamRepository _teamRepository;

        public TeamService(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }

        public void Add(Team team)
        {
            try
            {
                _teamRepository.Add(team);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Team team)
        {
            try
            {
                _teamRepository.Update(team);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Team FindById(int id)
        {
            try
            {
                return _teamRepository.FindById(id);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Delete(int id)
        {
            try
            {
                Team team = _teamRepository.FindById(id);

                team.IsDeleted = true;

                _teamRepository.Update(team);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Team> FindAll()
        {
            try
            {
                return _teamRepository.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
