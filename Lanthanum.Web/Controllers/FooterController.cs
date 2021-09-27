using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Lanthanum.Web.Services;
using System.Collections.Generic;
using Lanthanum.Web.Domain;
using System;

namespace Lanthanum.Web.Controllers
{
    public class FooterController : Controller
    {
        private readonly ILogger<FooterController> _logger;
        private readonly IFooterService _footerService;

        public FooterController(ILogger<FooterController> logger, IFooterService footerService)
        {
            _logger = logger;
            _footerService = footerService;
        }

        public IActionResult FooterConfiguration()
        {
            return View("FooterConfiguration", _footerService.GetAllItems());
        }

        public IActionResult RemoveItem(string itemName)
        {
            _footerService.RemoveItem(itemName);
            return View("FooterConfiguration");
        }

        public IActionResult HideItem(string itemName)
        {
            _footerService.HideItem(itemName);
            return View("FooterConfiguration");
        }

        public IActionResult UnhideItem(string itemName)
        {
            _footerService.UnhideItem(itemName);
            return View("FooterConfiguration");
        }

        [Route("FooterPage/{itemName}")]
        public IActionResult GetPage(string itemName)
        {
            return View("FooterPage", _footerService.GetSingleItem(itemName));
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
