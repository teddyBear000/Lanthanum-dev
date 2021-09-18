using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbRepository<User> _userRepository;

        public HomeController(ILogger<HomeController> logger, DbRepository<User> userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
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
