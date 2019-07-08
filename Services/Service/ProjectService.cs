using Data.Interfaces;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services
{
    public class ProjectService : IProjectService
    {
        private IProjectRepository _projectRepository;

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public void Add(Project team)
        {
            try
            {
                _projectRepository.Add(team);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Update(Project team)
        {
            try
            {
                _projectRepository.Update(team);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Project FindById(int id)
        {
            try
            {
                return _projectRepository.FindById(id);
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
                Project project = _projectRepository.FindById(id);

                project.IsDeleted = true;

                _projectRepository.Update(project);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Project> FindAll()
        {
            try
            {
                return _projectRepository.FindAll();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public IEnumerable<Project> FindByClientId(int clientId)
        {
            try
            {
                return _projectRepository.FindByClientId(clientId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
