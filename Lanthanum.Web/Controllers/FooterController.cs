using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Lanthanum.Web.Services;
using Lanthanum.Web.Domain;

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

        [Route("FooterPage/{itemName}")]
        public IActionResult GetPage(string itemName)
        {
            return View("FooterPage", _footerService.GetSingleItem(itemName));
        }

        [Route("Footer")]
        public IActionResult FooterConfiguration(string currentTab = "CompanyInfo")
        {
            ViewBag.currentTab = currentTab;
            return View("FooterConfiguration"+currentTab, _footerService.GetAllItems());
        }

        public IActionResult AddItem(string itemName, string currentTab)
        {
            ViewBag.currentTab = currentTab;
            _footerService.AddItem(new FooterItem { Name = itemName, Category = currentTab, Content = "", IsDisplaying = true });
            return View("FooterConfiguration" + currentTab);
        }

        public IActionResult UpdateItem(string itemName, string currentTab, string attributeToChange, string change)
        {
            ViewBag.currentTab = currentTab;
            _footerService.UpdateItem(itemName, attributeToChange, change);
            return View("FooterConfiguration" + currentTab);
        }

        public IActionResult RemoveItem(string itemName, string currentTab)
        {
            ViewBag.currentTab = currentTab;
            _footerService.RemoveItem(itemName);
            return View("FooterConfiguration"+currentTab);
        }

        public IActionResult HideItem(string itemName, string currentTab)
        {
            ViewBag.currentTab = currentTab;
            _footerService.HideItem(itemName);
            return View("FooterConfiguration" + currentTab);
        }

        public IActionResult UnhideItem(string itemName, string currentTab)
        {
            ViewBag.currentTab = currentTab;
            _footerService.UnhideItem(itemName);
            return View("FooterConfiguration" + currentTab);
        }

        [Route("ContactUs")]
        public IActionResult ContactUs()
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
