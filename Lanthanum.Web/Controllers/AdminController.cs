using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanthanum.Web.Controllers
{
    public class AdminController : Controller
    {
        [Route("admin/articles-list")]
        public IActionResult ArticlesList()
        {
            return View("articles_list");
        }
    }
}
