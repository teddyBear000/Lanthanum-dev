using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Lanthanum.Web.Data.Repositories;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Lanthanum.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly DbRepository<Article> _articleRepository;
        private readonly DbRepository<Team> _teamRepository;
        private readonly DbRepository<KindOfSport> _kindsRepository;
        private readonly IWebHostEnvironment _appEnvironment;

        public AdminController(IWebHostEnvironment appEnvironment, DbRepository<Article> articleRepository, DbRepository<Team> teamRepository, DbRepository<KindOfSport> kindsRepository)
        {
            _articleRepository = articleRepository;
            _teamRepository = teamRepository;
            _kindsRepository = kindsRepository;
            _appEnvironment = appEnvironment;
        }

        [HttpGet]
        public IActionResult Article() => View();

        [HttpPost]
        public IActionResult Article(string Conference, string Location, string Kindofsport, string Team, string Alt, string Headline, string Caption, string Content, IFormFile Logo)
        {
            var kindOfSport = _kindsRepository.Find(x => x.Name == Kindofsport).First();
            var team = _teamRepository.Find(x => x.Name == Team).First();

            var logoPath = " ";

            if (Logo != null)
            {
                string path = "/Files/" + Logo.FileName;
               
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    Logo.CopyToAsync(fileStream).Wait();
                }
                logoPath = path;

            }

            _articleRepository.AddAsync(new Article
            {
                Headline = Headline,
                Header = Caption,
                MainText = Content,
                Location = Location,
                Alternative = Alt,
                ViewsCount = 0,
                LogoPath = logoPath,
                CommentsCount = 0,
                DateTimeOfCreation = new DateTime(2021, 09, 13),
                //KindsOfSports = new List<KindOfSport>() { kindOfSport },
                //Teams = new List<Team>() { team },
            }).Wait();

            return RedirectToAction("Article");
        }
    }
}
