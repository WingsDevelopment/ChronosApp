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
using WebClient.Helpers.Enum;
using WebClient.Models.EmployeeViewModels;
using WebClient.Models.TimeShiftItemViewModels;

namespace WebClient.Controllers
{
    [Authorize]
    public class TimeShiftItemController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(TimeShiftItemController));

        private ITimeShiftItemService _timeShiftItemService;
        private IEmployeeService _employeeService;
        private IProjectService _projectService;
        private IReportService _reportService;

        public TimeShiftItemController(ITimeShiftItemService timeShiftItemService,
                                       IEmployeeService employeeService,
                                       IProjectService projectService,
                                       IReportService reportService)
        {
            _timeShiftItemService = timeShiftItemService;
            _employeeService = employeeService;
            _projectService = projectService;
            _reportService = reportService;
        }

        public IActionResult Index()
        {
            try
            {
                var userName = User.Identity.Name;

                Employee employee = _employeeService.FindByUserName(userName);

                TimeShiftItemViewModel model = new TimeShiftItemViewModel(employee);

                return View(model);
            }
            catch(Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                return View();
            }
        }

        [HttpPost]
        public IActionResult CreateTimeShiftItem(TimeShiftItemViewModel model)
        {

            try
            {
                if (!TimeHelper.IsBusinessDay(model.Date))
                {
                    TempData["Error"] = "Don't work on weekends, not healthy :(";
                    return RedirectToAction("Index");
                }
                else if (!ModelState.IsValid)
                {
                    TempData["Error"] = "Date is not in valid format.";
                    return RedirectToAction("Index");
                }
                Employee employee = _employeeService.FindByUserName(User.Identity.Name);

                TimeShiftItem timeShiftItem = new TimeShiftItem(employee.Id,
                                                                model.ProjectId,
                                                                model.Duration,
                                                                model.Date,
                                                                model.Discription,
                                                                model.IsBillable);
                _timeShiftItemService.Add(timeShiftItem);

                TempData["Success"] = TempDataMessages.MessageAfterCreate;
                return RedirectToAction("TimeShiftItemsThisMonth");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult UpdateTimeShiftItem(TimeShiftItemViewModel model)
        {
            if (!TimeHelper.IsBusinessDay(model.Date))
            {
                TempData["Error"] = "Don't work on weekends, not healthy :(";
                return RedirectToAction("TimeShiftItemsThisMonth");
            }
            else if (!ModelState.IsValid)
            {
                TempData["Error"] = "Date is not in valid format.";
                return RedirectToAction("TimeShiftItemsThisMonth");
            }
            try
            {
                TimeShiftItem timeShiftItem = new TimeShiftItem(model.EmployeeId,
                                                                model.ProjectId,
                                                                model.Duration,
                                                                model.Date,
                                                                model.Discription,
                                                                model.IsBillable);
                timeShiftItem.Id = model.Id;

                _timeShiftItemService.Update(timeShiftItem);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                return Json(new { success = false });
            }
        }

        public IActionResult DeleteTimeShiftItem(int id)
        {
            try
            {
                _timeShiftItemService.Delete(id);

                TempData["Success"] = TempDataMessages.MessageAfterDelete;
                return RedirectToAction("TimeShiftItemsThisMonth");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                TempData["Error"] = e.Message;
                return View();
            }
        }

        public IActionResult TimeShiftItemsThisMonth()
        {
            try
            {
                Employee employee = _employeeService.FindByUserName(User.Identity.Name);

                IEnumerable<TimeShiftItem> timeShiftItems = _timeShiftItemService.FindAllByEmployeeIdThisMonth(employee.Id);

                TimeShiftReportDTO dto = _reportService.GetTimeShiftReportThisMonth(employee, timeShiftItems);

                ListOfTimeShiftItemsViewModel model = new ListOfTimeShiftItemsViewModel(dto, employee);

                return View("TimeShiftItems", model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult TimeShiftItemsThisMonthChartView()
        {
            try
            {
                Employee employee = _employeeService.FindByUserName(User.Identity.Name);

                IEnumerable<TimeShiftItem> timeShiftItems = _timeShiftItemService.FindAllByEmployeeIdThisMonth(employee.Id);

                TimeShiftReportDTO dto = _reportService.GetTimeShiftReportThisMonth(employee, timeShiftItems);

                IEnumerable<Project> projects = _projectService.FindAll();

                ChartTimeShiftItemViewModel model = new ChartTimeShiftItemViewModel(dto, employee, projects, ChartType.Bar);

                return View("TimeShiftItemsChartView", model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult TimeShiftItemsForPeriod()
        {
            try
            {
                SelectListOfEmployeesViewModel model = new SelectListOfEmployeesViewModel();

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                TempData["Error"] = e.Message;
                return View();
            }
        }

        [HttpPost]
        public IActionResult TimeShiftItemsForPeriod(SelectListOfEmployeesViewModel model)
        {
            try
            {
                Employee employee = _employeeService.FindByUserName(User.Identity.Name);
                
                IEnumerable<TimeShiftItem> timeShiftItems =
                    _timeShiftItemService.FindAllByEmployeeIdForPeriod(employee.Id, model.DateFrom, model.DateTo);

                TimeShiftReportDTO dto = _reportService.GetTimeShiftReport(employee,
                                                                           timeShiftItems,
                                                                           model.DateFrom,
                                                                           model.DateTo);

                if (model.ViewType == ViewType.Table)
                {
                    ListOfTimeShiftItemsViewModel viewModel = new ListOfTimeShiftItemsViewModel(dto, employee);

                    return View("TimeShiftItems", viewModel);
                }
                else if (model.ViewType == ViewType.Chart)
                {
                    IEnumerable<Project> projects = _projectService.FindAll();

                    ChartTimeShiftItemViewModel viewModel = new ChartTimeShiftItemViewModel(dto, employee, projects, model.ChartType);

                    return View("TimeShiftItemsChartView", viewModel);
                }
                else
                {
                    TempData["Info"] = "Select type of view";
                    return RedirectToAction("TimeShiftItemsForPeriod");
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("TimeShiftItemsForPeriod");
            }
        }

        [HttpPost]
        public IActionResult TimeShiftItemsForPeriodForEmployee(int EmployeeId)
        {
            try
            {
                SelectListOfEmployeesViewModel viewModel = new SelectListOfEmployeesViewModel(EmployeeId);

                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpPost]
        public IActionResult TimeShiftItemsForPeriodForEmployeeAnalysis(SelectListOfEmployeesViewModel model)
        {
            try
            {
                Employee employee = _employeeService.FindById(model.EmployeeId);

                IEnumerable<TimeShiftItem> timeShiftItems =
                    _timeShiftItemService.FindAllByEmployeeIdForPeriod(employee.Id, model.DateFrom, model.DateTo);

                TimeShiftReportDTO dto = _reportService.GetTimeShiftReport(employee,
                                                                           timeShiftItems,
                                                                           model.DateFrom,
                                                                           model.DateTo);

                if (model.ViewType == ViewType.Table)
                {
                    ListOfTimeShiftItemsViewModel viewModel = new ListOfTimeShiftItemsViewModel(dto, employee);

                    return View("TimeShiftItems", viewModel);
                }
                else if (model.ViewType == ViewType.Chart)
                {
                    IEnumerable<Project> projects = _projectService.FindAll();

                    ChartTimeShiftItemViewModel viewModel = new ChartTimeShiftItemViewModel(dto, employee, projects, model.ChartType);

                    return View("TimeShiftItemsChartView", viewModel);
                }
                else
                {
                    TempData["Warning"] = "Select type of view";
                    return View();
                }
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(TimeShiftItemController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("TimeShiftItemsForPeriodForEmployee", new { EmployeeId  = model.EmployeeId});
            }
        }
    }
}
