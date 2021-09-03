using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
