using Microsoft.AspNetCore.Mvc;
using Lanthanum.Web.Services;
using Microsoft.Extensions.Logging;

namespace Lanthanum.Web.Controllers
{
    public class ArticlesController : Controller
    {

        private readonly IArticleService _articleService;
        private readonly ILogger<ArticlesController> _logger;

        public ArticlesController(IArticleService articleService, Logger<ArticlesController> logger)
        {
            _articleService = articleService;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}