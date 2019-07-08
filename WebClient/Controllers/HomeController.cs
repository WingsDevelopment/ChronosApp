using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebClient.Models;

namespace WebClient.Controllers
{
    public class HomeController : Controller
    {
        private static readonly log4net.ILog _logger = log4net.LogManager.GetLogger(typeof(HomeController));

        public HomeController()
        {
        }

        public IActionResult Index()
        {
            return View();
        }

        [Route("cats")]
        public IActionResult Cats()
        {
            if (string.IsNullOrEmpty(User.Identity.Name))
            {
                _logger.Debug("Somebody is watching cats :3 ");
            }
            else
            {
                _logger.Debug(User.Identity.Name + " is watching cats :3 ");
            }
            return View();
        }
    }
}
