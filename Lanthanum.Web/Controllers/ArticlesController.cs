using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lanthanum.Web.Controllers
{
    public class ArticlesController : Controller
    {
        private readonly DbRepository<Article> _articleRepositor;
        private readonly DbRepository<User> _userRepository;

        public ArticlesController(DbRepository<Article> articleRepositor, DbRepository<User> userRepository)
        {
            _articleRepositor = articleRepositor;
            _userRepository = userRepository;
        }

        public IActionResult Details(int id)
        {
            Article article = _articleRepositor.GetByIdAsync(id).Result;
            List<Article> articleList = _articleRepositor.GetAllAsync().Result.ToList();

            ViewBag.ModelArticle = article;
            ViewBag.ModelArticles = new List<Article>() { articleList[0], articleList[1], articleList[2], articleList[3], articleList[4], articleList[5] };
           
            return View();
        }
    }
}