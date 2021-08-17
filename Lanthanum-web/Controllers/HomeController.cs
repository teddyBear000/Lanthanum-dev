using Lanthanum_web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum_web.Data;
using Lanthanum_web.Domain;
using Microsoft.EntityFrameworkCore;

namespace Lanthanum_web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            Categories = new List<string>
            {
                "Home", "NBA", "NFL", "MLB", "CBB", "NASCAR", "GOLF", "SOCCER", "TEAM HUB", "LIFESTYLE", "DEALBOOK",
                "VIDEO"
            };
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

        public static List<string> Categories { get; set; }
    }
}
