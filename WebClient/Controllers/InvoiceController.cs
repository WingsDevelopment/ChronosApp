using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebClient.Helpers;
using WebClient.Models.InvoiceViewModels;

namespace WebClient.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class InvoiceController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(InvoiceController));

        private IInvoiceService _invoiceService;

        public InvoiceController(IInvoiceService invoiceService)
        {
            _invoiceService = invoiceService;
        }

        public IActionResult Index()
        {
            try
            {
                IEnumerable<Invoice> invoices = _invoiceService.FindAll();

                var model = new ListOfInvoicesViewModel(invoices);

                return View(model);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(InvoiceController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index", "Home");
            }
        }

        public IActionResult CreateInvoice()
        {
            try
            {
                InvoiceViewModel invoiceViewModel = new InvoiceViewModel();

                return View(invoiceViewModel);
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(InvoiceController)} error = {e}");
                TempData["Error"] = e.Message;
                return RedirectToAction("Index");
            }
        }
        [HttpPost]
        public IActionResult CreateInvoice(InvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Paid and demand fields range is 0.01 - max";
                return RedirectToAction("Index");
            }
            try
            {
                Invoice invoice = new Invoice(model.Demand,
                                              model.Paid,
                                              model.Discription);

                _invoiceService.Add(invoice);

                TempData["Success"] = TempDataMessages.MessageAfterCreate;
                return RedirectToAction("Index", "Invoice");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(InvoiceController)} error = {e}");
                TempData["Error"] = "Oopsy :(";
                return RedirectToAction("Index", "Invoice");
            }
        }

        public IActionResult UpdateInvoice(InvoiceViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Paid and demand fields range is 0.01 - max";
                return RedirectToAction("Index");
            }
            try
            {
                Invoice invoice = new Invoice(model.Demand,
                                              model.Paid,
                                              model.Discription,
                                              model.CreationDate);
                invoice.Id = model.Id;

                _invoiceService.Update(invoice);

                TempData["Success"] = TempDataMessages.MessageAfterUpdate;
                return Json(new { success = true });
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(InvoiceController)} error = {e}");
                return Json(new { success = false });
            }
        }
        public IActionResult DeleteInvoice(int id)
        {
            try
            {
                _invoiceService.Delete(id);

                TempData["Success"] = TempDataMessages.MessageAfterDelete;
                return RedirectToAction("Index", "Invoice");
            }
            catch (Exception e)
            {
                _logger.Error($"Error in {nameof(InvoiceController)} error = {e}");
                TempData["Success"] = "Woops!";
                return RedirectToAction("Index", "Invoice");
            }
        }
    }
}
