using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Lanthanum.Web.Controllers
{
    public class AdminController : Controller
    {
        [HttpGet]
        public IActionResult Article() => View();
        [HttpPost]
        public IActionResult Article(Article article) => Content($"{article.Headline}");
    }
}
