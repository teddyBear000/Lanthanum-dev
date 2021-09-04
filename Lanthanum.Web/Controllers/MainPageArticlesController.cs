using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanthanum.Web.Controllers
{
    public class MainPageArticlesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public List<Lanthanum.Web.Domain.Article> GetMainArticles()
        {
            return new List<Lanthanum.Web.Domain.Article> { };
        }
    }
}
