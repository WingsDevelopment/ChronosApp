using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.ExpenseViewModels;

namespace WebClient.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ExpenseController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(ExpenseController));

        private IExpenseService _expenseService;
        private IExpenseTypeService _expenseTypeService;

        public ExpenseController(IExpenseService expenseService, IExpenseTypeService expenseTypeService)
        {
            _expenseService = expenseService;
            _expenseTypeService = expenseTypeService;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<ExpenseType> expenseTypes = _expenseTypeService.FindAll();
                IEnumerable<Expense> expenses = _expenseService.FindAll();

                var model = new ListOfExpensesViewModel(expenses, expenseTypes);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult CreateExpense()
        {
            try
            {
                IEnumerable<ExpenseType> expenseTypes = _expenseTypeService.FindAll();

                ExpenseViewModel expenseViewModel = new ExpenseViewModel(expenseTypes);

                return View(expenseViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult CreateExpense(ExpenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill all reaquired fields";
                return RedirectToAction("Index");
            }
            try
            {
                Expense expense = new Expense(model.Date,
                                              model.Amount,
                                              model.ExpenseTypeId,
                                              model.MethodOfPayment);

                _expenseService.Add(expense);

                TempData["Success"] = TempDataMessages.MessageAfterCreate;
                return RedirectToAction("Index", "Expense");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseController)} error = {e}");
                TempData["Error"] = "Oopsy :(";
                return RedirectToAction("Index", "Expense");
            }
        }

        public IActionResult UpdateExpense(ExpenseViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Please fill all reaquired fields";
                return RedirectToAction("Index");
            }
            try
            {
                Expense expense = new Expense(model.Date,
                                              model.Amount,
                                              model.ExpenseTypeId,
                                              model.MethodOfPayment);
                expense.Id = model.Id;

                _expenseService.Update(expense);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseController)} error = {e}");
                return Json(new { success = false });
            }
        }
        public IActionResult DeleteExpense(int id)
        {
            try
            {
                _expenseService.Delete(id);

                TempData["Success"] = TempDataMessages.MessageAfterDelete;
                return RedirectToAction("Index", "Expense");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(ExpenseController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "Expense");
            }
        }
    }
}
