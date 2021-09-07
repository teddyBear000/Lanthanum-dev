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

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Details(int id)
        {
            Article article = _articleRepositor.GetByIdAsync(id).Result;
            var user = _userRepository.GetAllAsync().Result;

            ViewBag.ModelArticle = article;
            ViewBag.ModelArticles = new List<Article>() { article, article, article, article, article, article }; // TODO: Add real articles instead of one article
            ViewBag.Message = user;
           
            return View();
        }
    }
}