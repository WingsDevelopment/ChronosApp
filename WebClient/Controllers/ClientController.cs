using Chronos.Services.Interfaces;
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
using WebClient.Models.ClientViewModels;
using WebClient.Models.EmployeeViewModels;
using WebClient.Models.TimeShiftItemViewModels;

namespace WebClient.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ClientController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(ClientController));

        private IClientService _clientService;
        private ITimeShiftItemService _timeShiftItemService;
        private IReportService _reportService;
        private IProjectService _projectService;
        private IEmployeeService _employeeService;

        public ClientController(IClientService clientService,
                                ITimeShiftItemService timeShiftItemService,
                                IReportService reportService,
                                IProjectService projectService,
                                IEmployeeService employeeService)
        {
            _clientService = clientService;
            _timeShiftItemService = timeShiftItemService;
            _reportService = reportService;
            _projectService = projectService;
            _employeeService = employeeService;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<Client> projects = _clientService.FindAll();

                var model = new ListOfClientsViewModel(projects);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                return View();
            }
        }

        public IActionResult CreateClient()
        {
            try
            {
                ClientViewModel clientViewModel = new ClientViewModel();

                return View(clientViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                return View();
            }
        }
        [HttpPost]
        public IActionResult CreateClient(ClientViewModel clientViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Name is too long :(";
                return RedirectToAction("Index");
            }
            try
            {
                Client team = new Client(clientViewModel.ClientName);

                _clientService.Add(team);

                TempData["Success"] = TempDataMessages.MessageAfterCreate;
                return RedirectToAction("Index", "Client");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                TempData["Error"] = "Oopsy :(";
                return RedirectToAction("Index", "Client");
            }
        }

        public IActionResult UpdateClient(int id)
        {
            try
            {
                IEnumerable<Client> clients = _clientService.FindAll();

                Client client = _clientService.FindById(id);

                ClientViewModel teamViewModel = new ClientViewModel(client);

                return View(teamViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Client");
            }
        }
        [HttpPost]
        public IActionResult UpdateClient(ClientViewModel clientViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Name is too long :(";
                return RedirectToAction("Index");
            }
            try
            {
                Client client = new Client(clientViewModel.ClientName);
                client.Id = clientViewModel.Id;
                client.ClientName = clientViewModel.ClientName;

                _clientService.Update(client);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return RedirectToAction("Index", "Client");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Client");
            }
        }
        public IActionResult DeleteClient(int id)
        {
            try
            {
                _clientService.Delete(id);

                TempData["Success"] = TempDataMessages.MessageAfterDelete;
                return RedirectToAction("Index", "Client");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Client");
            }
        }

        public IActionResult TimeShiftItemsByClient()
        {
            try
            {
                IEnumerable<Client> clients = _clientService.FindAll();

                TimeShiftItemsByClientViewModel model = new TimeShiftItemsByClientViewModel(clients);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                TempData["Error"] = e.Message;
                return View();
            }
        }

        public IActionResult TimeShiftItemsChartForClientView(TimeShiftItemsByClientViewModel model)
        {
            try
            {
                IEnumerable<TimeShiftItem> timeShiftItems = 
                    _timeShiftItemService.FindAllByClientIdForPeriod(model.ClientId, model.DateFrom, model.DateTo);

                TimeShiftReportByClientDTO dto =
                    new TimeShiftReportByClientDTO(timeShiftItems,
                                                   timeShiftItems.Where(x => x.IsBillable).Sum(x => x.Duration),
                                                   timeShiftItems.Where(x => !x.IsBillable).Sum(x => x.Duration),
                                                   model.DateFrom,
                                                   model.DateTo);

                IEnumerable<Project> projects = _projectService.FindByClientId(model.ClientId);
                Client client = _clientService.FindById(model.ClientId);

                ChartTimeShiftItemForClientViewModel viewModel = new ChartTimeShiftItemForClientViewModel(dto, projects, client.ClientName);

                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("TimeShiftItemsByClient");
            }
        }

        public IActionResult TimeShiftItemsByClientPerEmployee()
        {
            try
            {
                IEnumerable<Client> clients = _clientService.FindAll();

                TimeShiftItemsByClientViewModel model = new TimeShiftItemsByClientViewModel(clients);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                TempData["Error"] = e.Message;
                return View();
            }
        }

        public IActionResult TimeShiftItemsChartForClientPerEmployeeView(TimeShiftItemsByClientViewModel model)
        {

            try
            {
                IEnumerable<TimeShiftItem> timeShiftItems =
                    _timeShiftItemService.FindAllByClientIdForPeriod(model.ClientId, model.DateFrom, model.DateTo);

                TimeShiftReportByClientDTO dto =
                    new TimeShiftReportByClientDTO(timeShiftItems,
                                                   timeShiftItems.Where(x => x.IsBillable).Sum(x => x.Duration),
                                                   timeShiftItems.Where(x => !x.IsBillable).Sum(x => x.Duration),
                                                   model.DateFrom,
                                                   model.DateTo);

                IEnumerable<Project> projects = _projectService.FindByClientId(model.ClientId);
                IEnumerable<Employee> employees = _employeeService.FindAll();
                Client client = _clientService.FindById(model.ClientId);

                ChartTimeShiftItemForClientPerEmployeeViewModel viewModel =
                    new ChartTimeShiftItemForClientPerEmployeeViewModel(dto, projects, employees, client.ClientName);

                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ClientController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("TimeShiftItemsByClient");
            }
        }
    }
}
