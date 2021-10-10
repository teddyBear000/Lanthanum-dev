using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using System.Collections.Generic;

namespace Lanthanum.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DbRepository<User> _userRepository;
        private readonly DbRepository<Article> _articleRepository;
        private readonly DbRepository<Comment> _commentRepository;
        public HomeController(ILogger<HomeController> logger, DbRepository<User> userRepository, DbRepository<Article> articleRepository, DbRepository<Comment> commentRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
            Categories = new List<string>
            {
                "Home", "NBA", "NFL", "MLB", "CBB", "NASCAR", "GOLF", "SOCCER", "TEAM HUB", "LIFESTYLE", "DEALBOOK",
                "VIDEO"
            };
            _articleRepository = articleRepository;
            _commentRepository = commentRepository;

        }
        public IActionResult Index()
        {


            var articles = new List<Article>();
            foreach (var articleElement in _articleRepository.GetAllAsync().Result)
            {
                articles.Add(articleElement);
            }

            var additionalArticles = new List<Article>();
            foreach (var additionalArticleElement in _articleRepository.GetAllAsync().Result)
            {
                additionalArticles.Add(additionalArticleElement);
            }

            var model = new MainPageArticlesViewModel
            {
                MainArticles = articles,
                AdditionalArticles = additionalArticles
            };
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        
        // TODO: Move
        private void AddAdmin()
        {
            const string email = "mail@gmail.com"; // admin email
            if (_userRepository.SingleOrDefaultAsync(x => x.Email == email).Result == null)
            {
                _userRepository.AddAsync(new User
                {
                    Email = email,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("12345678"),
                    Role = RoleStates.Admin
                }).Wait();
            }
        }

        public static List<string> Categories { get; set; }
    }
}