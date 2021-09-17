using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Lanthanum.Web.Domain;
using System.Collections.Generic;

namespace Lanthanum.Web.Controllers
{
    public class FooterController : Controller
    {
        private readonly ILogger<FooterController> _logger;

        public FooterController(ILogger<FooterController> logger)
        {
            _logger = logger;
        }

        [Route("About")]
        public IActionResult About()
        {
            return View();
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
        {
            return View();
        }

        [Route("Privacy")]
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
