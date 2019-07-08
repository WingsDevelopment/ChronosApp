using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.ExpenseTypeViewModels;

namespace WebExpenseType.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ExpenseTypeController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(ExpenseTypeController));

        private IExpenseTypeService _expenseTypeService;

        public ExpenseTypeController(IExpenseTypeService expenseTypeService)
        {
            _expenseTypeService = expenseTypeService;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<ExpenseType> projects = _expenseTypeService.FindAll();

                var model = new ListOfExpenseTypesViewModel(projects);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseTypeController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult CreateExpenseType()
        {
            try
            {
                ExpenseTypeViewModel expenseTypeViewModel = new ExpenseTypeViewModel();

                return View(expenseTypeViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseTypeController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult CreateExpenseType(ExpenseTypeViewModel expenseTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill all reaquired fields";
                return RedirectToAction("Index");
            }
            try
            {
                ExpenseType team = new ExpenseType(expenseTypeViewModel.ExpenseTypeName, expenseTypeViewModel.Discription);

                _expenseTypeService.Add(team);

                TempData["Success"] = TempDataMessages.MessageAfterCreate;
                return RedirectToAction("Index", "ExpenseType");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseTypeController)} error = {e}");
                TempData["Error"] = "Oopsy :(";
                return RedirectToAction("Index", "ExpenseType");
            }
        }

        public IActionResult UpdateExpenseType(int id)
        {
            try
            {
                IEnumerable<ExpenseType> expenseTypes = _expenseTypeService.FindAll();

                ExpenseType expenseType = _expenseTypeService.FindById(id);

                ExpenseTypeViewModel teamViewModel = new ExpenseTypeViewModel(expenseType);

                return View(teamViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseTypeController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "ExpenseType");
            }
        }
        [HttpPost]
        public IActionResult UpdateExpenseType(ExpenseTypeViewModel expenseTypeViewModel)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill all reaquired fields";
                return RedirectToAction("Index");
            }
            try
            {
                ExpenseType expenseType = new ExpenseType(expenseTypeViewModel.ExpenseTypeName, 
                                                          expenseTypeViewModel.Discription);
                expenseType.Id = expenseTypeViewModel.Id;
                expenseType.ExpenseTypeName = expenseTypeViewModel.ExpenseTypeName;

                _expenseTypeService.Update(expenseType);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return RedirectToAction("Index", "ExpenseType");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseTypeController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "ExpenseType");
            }
        }
        public IActionResult DeleteExpenseType(int id)
        {
            try
            {
                _expenseTypeService.Delete(id);

                TempData["Success"] = TempDataMessages.MessageAfterDelete;
                return RedirectToAction("Index", "ExpenseType");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseTypeController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "ExpenseType");
            }
        }
    }
}
