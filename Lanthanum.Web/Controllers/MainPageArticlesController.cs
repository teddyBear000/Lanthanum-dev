using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;

namespace Lanthanum.Web.Controllers
{
    public class MainPageArticlesController : Controller
    {

        private readonly DbRepository<Article> _articleRepository;

        public MainPageArticlesController(DbRepository<Article> articleRepository) 
        {
            //context.AddRange(data);

            _articleRepository = articleRepository;
        }
        public static List<Article> GetMainArticles()
        {
            var data = new List<Lanthanum.Web.Domain.Article> {
                new Domain.Article {
                    Authors = new List<Domain.User>{
                        new Domain.User { Nickname = "nick" }
                    },
                    Headline = "London Games return in 2019",
                    DateTimeOfCreation = new DateTime(2019, 09, 20),
                    LogoPath = "/images/download2.jpg",
                    MainText = "Register to receive the latest news on ticket sales for the four NFL London Games in 2019!"

                },
                new Domain.Article {
                    Authors = new List<Domain.User>{
                        new Domain.User { Nickname = "nick1" }
                    },
                    Headline = "London Games return in 2020",
                    DateTimeOfCreation = new DateTime(2019, 12, 15),
                    LogoPath = "/images/auth-basketball.jpg",
                    MainText = "Text2"
                },
                new Domain.Article {
                    Authors = new List<Domain.User>{
                        new Domain.User { Nickname = "nick1" }
                    },
                    Headline = "London Games return in 2021",
                    DateTimeOfCreation = new DateTime(2020, 01, 11),
                    LogoPath = "/images/istockphoto-913331746-612x612.jpg",
                    MainText = "BLABLALBA BLA bAL bBLA"
                }
            };
            //var articles = _articleRepository.GetAllAsync().Result.ToList();
            var articles = data;
            return articles;
        }
    }
}
