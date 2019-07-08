using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.TeamViewModels;

namespace WebClient.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class TeamController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(TeamController));

        private ITeamService _teamService;
        private IProjectService _projectService;

        public TeamController(ITeamService teamService, IProjectService projectService)
        {
            _teamService = teamService;
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<Team> teams = _teamService.FindAll();

                var model = new ListOfTeamsViewModel(teams);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TeamController)} error = {e}");
                return View();
            }
        }

        public IActionResult CreateTeam()
        {
            try
            {
                IEnumerable<Project> projects = _projectService.FindAll();

                TeamViewModel teamViewModel = new TeamViewModel(projects);

                return View(teamViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TeamController)} error = {e}");
                return View();
            }
        }
        [HttpPost]
        public IActionResult CreateTeam(TeamViewModel teamViewModel)
        {
            try
            {
                Team team = new Team(teamViewModel.ProjectId, teamViewModel.TeamName);

                _teamService.Add(team);

                TempData["Success"] = TempDataMessages.MessageAfterCreate;
                return RedirectToAction("Index", "Team");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TeamController)} error = {e}");
                TempData["Error"] = "Oopsy :(";
                return RedirectToAction("Index", "Team");
            }
        }

        public IActionResult UpdateTeam(int id)
        {
            try
            {
                IEnumerable<Project> projects = _projectService.FindAll();

                Team team = _teamService.FindById(id);

                TeamViewModel teamViewModel = new TeamViewModel(team, projects);

                return View(teamViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TeamController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "Team");
            }
        }
        [HttpPost]
        public IActionResult UpdateTeam(TeamViewModel teamViewModel)
        {
            try
            {
                Team team = new Team(teamViewModel.ProjectId, teamViewModel.TeamName);
                team.Id = teamViewModel.Id;
                team.TeamName = teamViewModel.TeamName;
                team.ProjectId = teamViewModel.ProjectId;

                _teamService.Update(team);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return RedirectToAction("Index", "Team");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TeamController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "Team");
            }
        }
        public IActionResult DeleteTeam(int id)
        {
            try
            {
                _teamService.Delete(id);

                TempData["Success"] = TempDataMessages.MessageAfterDelete;
                return RedirectToAction("Index", "Team");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TeamController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "Team");
            }
        }
    }
}
