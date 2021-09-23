using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using Lanthanum.Web.Models;
using System.Threading.Tasks;

namespace Lanthanum.Web.Controllers
{
    public class ArticlesController : Controller
    {

        private readonly ILogger<ArticlesController> _logger;
        private readonly DbRepository<Article> _articleRepository;

        public ArticlesController(ILogger<ArticlesController> logger, DbRepository<Article> articleRepository)
        {
            _logger = logger;
            _articleRepository = articleRepository;
        }

        public IActionResult Details(int id)
        {
            var article =  _articleRepository.GetByIdAsync(id).Result;

            List<Article> articleList = _articleRepository.GetAllAsync().Result.ToList();

            var model = new ArticleViewModel
            {
                MainArticle = article,
                MoreArticlesSection = new List<Article>() { articleList[0], articleList[0], articleList[0], articleList[0], articleList[0], articleList[0] }
            };
           
            return View(model);
        }
    }
}