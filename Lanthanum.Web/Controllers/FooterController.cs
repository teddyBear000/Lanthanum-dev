using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Lanthanum.Web.Services;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Authorization;

namespace Lanthanum.Web.Controllers
{
    //[Authorize]
    public class FooterController : Controller
    {
        private readonly ILogger<FooterController> _logger;
        private readonly IFooterService _footerService;

        public FooterController(ILogger<FooterController> logger, IFooterService footerService)
        {
            _logger = logger;
            _footerService = footerService;
        }

        [Route("Footer/Page/{itemName}")]
        public IActionResult GetPage(string itemName)
        {
            return View("FooterPage", _footerService.GetSingleItem(itemName));
        }

        [Route("Footer")]
        public IActionResult FooterConfiguration(string itemName = "About Sports Hub", string currentTab = "CompanyInfo")
        {
            ViewBag.currentTab = currentTab;
            if (itemName == "")
            {
                switch (currentTab)
                {
                    case "CompanyInfo":
                        ViewBag.itemName = "About Sports Hub";
                        break;
                    case "Contributors":
                        ViewBag.itemName = "Featured Writers Program";
                        break;
                    case "Newsletter":
                        ViewBag.itemName = "Sign up to receive the latest sports news";
                        break;
                }
            }
            else { ViewBag.itemName = itemName; }
            
            return View("FooterConfiguration" + currentTab, _footerService.GetAllItems());
        }

        [HttpPost]
        public IActionResult AddItem(string itemNameForAdd, string currentTabForAdd)
        {
            ViewBag.currentTab = currentTabForAdd;
            ViewBag.itemName = itemNameForAdd;
            _footerService.AddItem(new FooterItem { Name = itemNameForAdd, Category = currentTabForAdd, Content = "", IsDisplaying = true });
            return RedirectToAction("FooterConfiguration", new { itemName = itemNameForAdd, currentTab = currentTabForAdd });
        }

        [HttpPost]
        public IActionResult UpdateItem(string itemNameForUpdate, string nameChangeForUpdate, string contentChangeForUpdate, string currentTabForUpdate)
        {
            ViewBag.currentTab = currentTabForUpdate;
            ViewBag.itemName = nameChangeForUpdate;
            _footerService.UpdateItem(itemNameForUpdate, nameChangeForUpdate, contentChangeForUpdate);
            return RedirectToAction("FooterConfiguration", new { itemName = nameChangeForUpdate, currentTab = currentTabForUpdate });
        }

        [HttpPost]
        public IActionResult UpdateContactUs(string addressForUpdate, string telForUpdate, string emailForUpdate)
        {
            var itemNameForUpdate = "Contact Us";
            var currentTabForUpdate = "CompanyInfo";
            ViewBag.currentTab = itemNameForUpdate;
            ViewBag.itemName = currentTabForUpdate;
            var contentChangeForUpdate = addressForUpdate+","+telForUpdate+","+emailForUpdate;
            _footerService.UpdateItem(itemNameForUpdate, itemNameForUpdate, contentChangeForUpdate);
            return RedirectToAction("FooterConfiguration", new { itemName = itemNameForUpdate, currentTab = currentTabForUpdate });
        }

        public IActionResult RemoveItem(string itemName, string currentTab)
        {
            ViewBag.currentTab = currentTab;
            switch (currentTab)
            {
                case "CompanyInfo":
                    ViewBag.itemName = "About Sports Hub";
                    break;
                case "Contributors":
                    ViewBag.itemName = "Featured Writers Program";
                    break;
                case "Newsletter":
                    ViewBag.itemName = "Sign up to receive the latest sports news";
                    break;
            }
            _footerService.RemoveItem(itemName);
            return View("FooterConfiguration" + currentTab);
        }

        public IActionResult HideUnhideItem(string itemName, string currentTab)
        {
            ViewBag.currentTab = currentTab;
            ViewBag.itemName = itemName;
            _footerService.HideUnhideItem(itemName);
            return View("FooterConfiguration" + currentTab);
        }

        public IActionResult HideUnhideAllItemsInCategory(string currentTab)
        {
            ViewBag.currentTab = currentTab;
            switch (currentTab)
            {
                case "CompanyInfo":
                    ViewBag.itemName = "About Sports Hub";
                    break;
                case "Contributors":
                    ViewBag.itemName = "Featured Writers Program";
                    break;
                case "Newsletter":
                    ViewBag.itemName = "Sign up to receive the latest sports news";
                    break;
            }
            _footerService.HideUnhideAllItemsInCategory(currentTab);
            return View("FooterConfiguration" + currentTab);
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

        [Route("Terms")]
        public IActionResult Terms()
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
