using Domain.DTOs;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.ProjectViewModels;

namespace WebClient.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProjectController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(ProjectController));

        private IClientService _clientService;
        private IProjectService _projectService;
        private ITimeShiftItemService _timeShiftItemService;
        private IEmployeeService _employeeService;

        public ProjectController(IClientService clientService,
                                 IProjectService projectService,
                                 ITimeShiftItemService timeShiftItemService,
                                 IEmployeeService employeeService)
        {
            _clientService = clientService;
            _projectService = projectService;
            _timeShiftItemService = timeShiftItemService;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<Project> projects = _projectService.FindAll();

                var model = new ListOfProjectsViewModel(projects);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ProjectController)} error = {e}");
                return View();
            }
        }

        public IActionResult CreateProject()
        {
            try
            {
                IEnumerable<Client> clients = _clientService.FindAll();

                ProjectViewModel teamViewModel = new ProjectViewModel(clients);

                return View(teamViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ProjectController)} error = {e}");
                return View();
            }
        }
        [HttpPost]
        public IActionResult CreateProject(ProjectViewModel teamViewModel)
        {
            try
            {
                Project team = new Project(teamViewModel.ClientId, teamViewModel.ProjectName);

                _projectService.Add(team);

                TempData["Success"] = TempDataMessages.MessageAfterCreate;
                return RedirectToAction("Index", "Project");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ProjectController)} error = {e}");
                TempData["Error"] = "Oopsy :(";
                return RedirectToAction("Index", "Project");
            }
        }

        public IActionResult UpdateProject(int id)
        {
            try
            {
                IEnumerable<Client> clients = _clientService.FindAll();

                Project project = _projectService.FindById(id);

                ProjectViewModel teamViewModel = new ProjectViewModel(project, clients);

                return View(teamViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ProjectController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "Project");
            }
        }
        [HttpPost]
        public IActionResult UpdateProject(ProjectViewModel teamViewModel)
        {
            try
            {
                Project project = new Project(teamViewModel.ClientId, teamViewModel.ProjectName);
                project.Id = teamViewModel.Id;
                project.ProjectName = teamViewModel.ProjectName;
                project.ClientId = teamViewModel.ClientId;

                _projectService.Update(project);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return RedirectToAction("Index", "Project");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ProjectController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "Project");
            }
        }
        public IActionResult DeleteProject(int id)
        {
            try
            {
                _projectService.Delete(id);

                TempData["Success"] = TempDataMessages.MessageAfterDelete;
                return RedirectToAction("Index", "Project");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ProjectController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "Project");
            }
        }

        public IActionResult TimeShiftItemsByProject()
        {
            try
            {
                IEnumerable<Project> projects = _projectService.FindAll();

                TimeShiftItemsByProjectViewModel model = new TimeShiftItemsByProjectViewModel(projects);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ProjectController)} error = {e}");
                return View();
            }
        }
        public IActionResult TimeShiftItemsChartForProjectView(TimeShiftItemsByProjectViewModel model)
        {
            try
            {
                IEnumerable<TimeShiftItem> timeShiftItems =
                    _timeShiftItemService.FindAllByProjectIdForPeriod(model.ProjectId, model.DateFrom, model.DateTo);

                TimeShiftReportByClientDTO dto =
                    new TimeShiftReportByClientDTO(timeShiftItems,
                                                   timeShiftItems.Where(x => x.IsBillable).Sum(x => x.Duration),
                                                   timeShiftItems.Where(x => !x.IsBillable).Sum(x => x.Duration),
                                                   model.DateFrom,
                                                   model.DateTo);

                Project project = _projectService.FindById(model.ProjectId);
                IEnumerable<Employee> employees = _employeeService.FindAll(); // optimizovati filter po projectu

                ChartTimeShiftItemForProjectViewModel viewModel = new ChartTimeShiftItemForProjectViewModel(dto, project, employees);

                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ProjectController)} error = {e}");
                TempData["Error"] = "Woops";
                return RedirectToAction("TimeShiftItemsByProject");
            }
        }
        public IActionResult TimeShiftItemsChartForProjectPerEmployeeView(TimeShiftItemsByProjectViewModel model)
        {

            try
            {
                IEnumerable<TimeShiftItem> timeShiftItems =
                    _timeShiftItemService.FindAllByProjectIdForPeriod(model.ProjectId, model.DateFrom, model.DateTo);

                TimeShiftReportByClientDTO dto =
                    new TimeShiftReportByClientDTO(timeShiftItems,
                                                   timeShiftItems.Where(x => x.IsBillable).Sum(x => x.Duration),
                                                   timeShiftItems.Where(x => !x.IsBillable).Sum(x => x.Duration),
                                                   model.DateFrom,
                                                   model.DateTo);

                Project project = _projectService.FindById(model.ProjectId);
                IEnumerable<Employee> employees = _employeeService.FindAll();

                ChartTimeShiftItemForProjectPerEmployeeViewModel viewModel =
                    new ChartTimeShiftItemForProjectPerEmployeeViewModel(dto, project, employees);

                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ProjectController)} error = {e}");
                TempData["Error"] = "Woops";
                return RedirectToAction("TimeShiftItemsByProject");
            }
        }
    }
}
