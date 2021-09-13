using Microsoft.AspNetCore.Mvc;

namespace Lanthanum.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Article() => View();
        [HttpPost]
        public IActionResult Article(string name) => Content($"{name}");
    }
}
