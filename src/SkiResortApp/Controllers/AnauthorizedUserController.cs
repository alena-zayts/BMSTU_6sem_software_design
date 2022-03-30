using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SkiResortApp.ComponentAccessToDB.DBModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SkiResortApp.Controllers
{
    public class UnauthorizedUserController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public UnauthorizedUserController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
