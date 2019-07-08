using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.OffTimeViewModels;

namespace WebClient.Controllers
{
    [Authorize]
    public class OffTimeController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(OffTimeController));

        private IOffTimeService _offTimeService;
        private IEmployeeService _employeeService;
        private IProjectService _projectService;

        public OffTimeController(IOffTimeService offTimeService,
                                       IEmployeeService employeeService,
                                       IProjectService projectService)
        {
            _offTimeService = offTimeService;
            _employeeService = employeeService;
            _projectService = projectService;
        }

        public IActionResult Index()
        {
            try
            {
                OffTimeViewModel model = new OffTimeViewModel();

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(OffTimeController)} error = {e}");
                return View();
            }
        }

        [HttpPost]
        public IActionResult CreateOffTime(OffTimeViewModel model)
        {
            try
            {
                Employee employee = _employeeService.FindByUserName(User.Identity.Name);

                _offTimeService.AddManyBetweenDates(employee.Id,
                                                    model.OffTimeType,
                                                    model.Discription,
                                                    model.DateFrom,
                                                    model.DateTo);

                TempData["Success"] = TempDataMessages.MessageAfterCreate;
                return RedirectToAction("OffTimesAllTime");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(OffTimeController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult UpdateOffTime(OffTimeViewModel model)
        {
            try
            {
                OffTime timeShiftItem = new OffTime(model.EmployeeId,
                                                    model.Date,
                                                    model.OffTimeType,
                                                    model.Discription);
                timeShiftItem.Id = model.Id;

                _offTimeService.Update(timeShiftItem);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(OffTimeController)} error = {e}");
                return Json(new { success = false, message = e.Message });
            }
        }

        public IActionResult DeleteOffTime(int id)
        {
            try
            {
                _offTimeService.Delete(id);

                TempData["Success"] = TempDataMessages.MessageAfterDelete;
                return RedirectToAction("OffTimesThisMonth");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(OffTimeController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApproveOffTime(int id)
        {
            try
            {
                OffTime offTime = _offTimeService.FindById(id);

                offTime.IsApproved = true;

                _offTimeService.Update(offTime);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return Json(new { success = true, message = TempDataMessages.MessageAfterUpdate });
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(OffTimeController)} error = {e}");
                return Json(new { success = false, message = e.Message });
            }
        }
        
        public IActionResult OffTimesAllTime(int EmployeeId)
        {
            try
            {
                Employee employee = _employeeService.FindByUserName(User.Identity.Name);

                IEnumerable<Project> projects = _projectService.FindAll();
                IEnumerable<OffTime> offTimes =
                    _offTimeService.FindAllByEmployeeId(employee.Id);

                ListOfOffTimesViewModel model = new ListOfOffTimesViewModel(offTimes, employee.UserName);

                return View("OffTimes", model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(OffTimeController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult OffTimesAllTimeForEmployee(int employeeId)
        {
            try
            {
                Employee employee = _employeeService.FindById(employeeId);

                IEnumerable<Project> projects = _projectService.FindAll();
                IEnumerable<OffTime> offTimes =
                    _offTimeService.FindAllByEmployeeId(employee.Id);

                ListOfOffTimesViewModel model = new ListOfOffTimesViewModel(offTimes, employee.UserName);

                return View("OffTimes", model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(OffTimeController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult OffTimesThisMonth()
        {
            try
            {
                Employee employee = _employeeService.FindByUserName(User.Identity.Name);

                IEnumerable<Project> projects = _projectService.FindAll();
                IEnumerable<OffTime> offTimes = 
                    _offTimeService.FindAllByEmployeeIdThisMonth(employee.Id);

                ListOfOffTimesViewModel model = new ListOfOffTimesViewModel(offTimes, employee.UserName);

                return View("OffTimes", model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(OffTimeController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult OffTimesForPeriod()
        {
            return View();
        }

        [HttpPost]
        public IActionResult OffTimesForPeriod(DateTime from, DateTime to)
        {
            try
            {
                Employee employee = _employeeService.FindByUserName(User.Identity.Name);

                IEnumerable<Project> projects = _projectService.FindAll();
                IEnumerable<OffTime> timeShiftItems =
                    _offTimeService.FindAllByEmployeeIdForPeriod(employee.Id, from, to);

                ListOfOffTimesViewModel model = new ListOfOffTimesViewModel(timeShiftItems, employee.UserName);

                return View("OffTimes", model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(OffTimeController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }
    }
}
