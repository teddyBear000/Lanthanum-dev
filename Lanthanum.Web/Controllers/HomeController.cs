using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Microsoft.EntityFrameworkCore;
using Lanthanum.Web.Services;

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


            var task1 = _articleRepository.AddAsync(new Domain.Article
            {
                Authors = new List<Domain.User>{
                        new Domain.User { Nickname = "nick" }
                    },
                Headline = "London Games return in 2019",
                DateTimeOfCreation = new DateTime(2019, 09, 20),
                LogoPath = "/images/download2.jpg",
                MainText = "Register to receive the latest news on ticket sales for the four NFL London Games in 2019!"

            });
            task1.Wait();

            var task2 = _articleRepository.AddAsync(
                new Article
                {
                    Authors = new List<Domain.User>{
                        new Domain.User { Nickname = "nick1" }
                    },
                    Headline = "London Games return in 2020",
                    DateTimeOfCreation = new DateTime(2019, 12, 15),
                    LogoPath = "/images/auth-basketball.jpg",
                    MainText = "Text2"
                }
                );
            task2.Wait();

            var task3 = _articleRepository.AddAsync(
            new Domain.Article
            {
                Authors = new List<Domain.User>{
                        new Domain.User { Nickname = "nick1" }
                    },
                Headline = "London Games return in 2021",
                DateTimeOfCreation = new DateTime(2020, 01, 11),
                LogoPath = "/images/download2.jpg",
                MainText = "BLABLALBA BLA bAL bBLA"
            });

            task3.Wait();

            var task4 = _articleRepository.AddAsync(
            new Domain.Article
            {
                Authors = new List<Domain.User>{
                        new Domain.User { Nickname = "nick1" }
                    },
                Headline = "London Games return in 2021",
                DateTimeOfCreation = new DateTime(2020, 01, 11),
                LogoPath = "/images/download2.jpg",
                MainText = "BLABLALBA BLA bAL bBLA"
            });

            task4.Wait();
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