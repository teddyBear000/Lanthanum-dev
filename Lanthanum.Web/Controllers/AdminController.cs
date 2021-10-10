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
using Lanthanum.Web.Data.Domain;
using Lanthanum.Web.Models;

namespace Lanthanum.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly DbRepository<Article> _articleRepository;
        private readonly DbRepository<Team> _teamRepository;
        private readonly DbRepository<KindOfSport> _kindsRepository;
        private readonly DbRepository<Picture> _pictureRepository;
        private readonly DbRepository<Conference> _conferenceRepository;
        private readonly IWebHostEnvironment _appEnvironment;

        public AdminController(IWebHostEnvironment appEnvironment, DbRepository<Picture> pictureRepository, DbRepository<Article> articleRepository, DbRepository<Team> teamRepository, DbRepository<KindOfSport> kindsRepository, DbRepository<Conference> conferenceRepository)
        {
            _pictureRepository = pictureRepository;
            _articleRepository = articleRepository;
            _teamRepository = teamRepository;
            _kindsRepository = kindsRepository;
            _appEnvironment = appEnvironment;
            _conferenceRepository = conferenceRepository;
        }

        [HttpGet]
        public IActionResult Article(string selected="none") 
        {
            var sport = "NBA";

            if (selected != "none")
            {
                sport = selected;
            }

            var model = new AddingArticleViewModel 
            {
                SelectedSport = sport,
                Teams = _teamRepository.GetAllAsync().Result.ToList(),
                Conferences = _conferenceRepository.GetAllAsync().Result.ToList(),
                KindsOfSport = _kindsRepository.GetAllAsync().Result.ToList()
            };

            return View(model); 
        }


        [HttpPost]
        public IActionResult Article(string Conference, string Location, string Kindofsport, string Team, string Alt, string Headline, string Caption, string Content, string Filter, string Size, string Crop, IFormFile Logo)
        {
            var kindOfSport = _kindsRepository.Find(x => x.Name == Kindofsport).First();
            var team = _teamRepository.Find(x => x.Name == Team).First();
            var conference = _conferenceRepository.Find(x => x.Name == Conference).First();

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

            _pictureRepository.AddAsync(new Picture
            {
                LogoPath = logoPath,
                Filter = Filter,
                Size = Size,
                Crop = Crop
            }).Wait();

            Picture picture = _pictureRepository.Find(x => logoPath == x.LogoPath).First();

            _articleRepository.AddAsync(new Article
            {
                Headline = Headline,
                Header = Caption,
                HTMLText = Content,
                MainText = HtmlEditor(Content),
                Location = Location,
                Alternative = Alt,
                ViewsCount = 0,
                CommentsCount = 0,
                KindsOfSports = new List<KindOfSport>() { kindOfSport },
                Teams = new List<Team>() { team },
                LogoPicture = picture,
                Conference = conference
            }).Wait();

            return RedirectToAction("Article");
        }

        private static string HtmlEditor(string text)
        {
            string result = "";

            bool IsExtra = false;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == '<')
                {
                    IsExtra = true;
                }
                else if (text[i] == '>')
                {
                    IsExtra = false;
                }
                else if (!IsExtra)
                {
                    result += text[i];
                }
            }

            return result;
        }
    }
}
