using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.EmployeeViewModels;

namespace WebClient.Controllers
{
    [Authorize]
    [Authorize(Roles = "Administrator")]
    public class EmployeeController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(EmployeeController));

        private IEmployeeService _employeeService;
        private IProjectService _projectService;
        private ITeamService _teamService;

        public EmployeeController(IEmployeeService employeeService, IProjectService projectService, ITeamService teamService)
        {
            _employeeService = employeeService;
            _projectService = projectService;
            _teamService = teamService;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<Employee> employees = _employeeService.FindAll();

                var model = new ListOfEmployeesViewModel(employees);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(EmployeeController)} error = {e}");
                return View();
            }
        }

        public IActionResult UpdateEmployee(int id)
        {
            try
            {
                Employee employee = _employeeService.FindById(id);

                IEnumerable<Project> projects = _projectService.FindAll();
                IEnumerable<Team> teams = _teamService.FindAll();

                EmployeeViewModel employeeViewModel = new EmployeeViewModel(employee, projects, teams);

                return View(employeeViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(EmployeeController)} error = {e}");
                TempData["Error"] = "Woops!";
                return RedirectToAction("Index", "Employee");
            }
        }
        [HttpPost]
        public IActionResult UpdateEmployee(EmployeeViewModel employeeViewModel)
        {
            if (!ModelState.IsValid)
            {
                string errors = string.Empty;
                foreach (var modelState in ViewData.ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        errors += error.ErrorMessage + Environment.NewLine;
                    }
                }
                TempData["Error"] = errors;
                return RedirectToAction("Index");
            }
            try
            {
                Employee employee =
                    _employeeService.FindById(employeeViewModel.Id);

                employee.MainProjectId = employeeViewModel.MainProjectId;
                employee.PriceLevel = employeeViewModel.PriceLevel;
                employee.HourPerDay = employeeViewModel.HourPerDay;
                employee.TeamdId = employeeViewModel.TeamdId;

                _employeeService.Update(employee);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(EmployeeController)} error = {e}");
                TempData["Error"] = "Woops!";
                return RedirectToAction("Index", "Employee");
            }
        }

        public IActionResult AddEmployeeToProject(int employeeId, int projectId)
        {
            try
            {
                _employeeService.AddEmployeeToProject(employeeId, projectId);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return RedirectToAction("UpdateEmployee", new { id = employeeId } );
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(EmployeeController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("UpdateEmployee", new { id = employeeId });
            }
        }

        [HttpPost]
        public IActionResult RemoveEmployeeFromProject(int EmployeeId, int ProjectId)
        {
            try
            {
                _employeeService.RemoveEmployeeFromProject(EmployeeId, ProjectId);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return RedirectToAction("UpdateEmployee", new { id = EmployeeId });
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(EmployeeController)} error = {e}");
                return RedirectToAction("UpdateEmployee", new { id = EmployeeId });
            }
        }

        public IActionResult DeleteEmployee(int id)
        {
            try
            {
                _employeeService.Delete(id);

                TempData["Success"] = TempDataMessages.MessageAfterDelete;
                return RedirectToAction("Index", "Employee");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(EmployeeController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "Employee");
            }
        }
    }
}
