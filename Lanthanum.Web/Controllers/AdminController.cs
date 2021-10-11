using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Lanthanum.Web.Data.Repositories;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Lanthanum.Web.Data.Domain;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using System.Linq;

namespace Lanthanum.Web.Controllers
{
    //[Authorize] //TODO: Change to [Authorize(Role="Admin")]
    public class AdminController : Controller
    {
        private readonly DbRepository<Article> _articleRepository;
        private readonly DbRepository<Team> _teamRepository;
        private readonly DbRepository<KindOfSport> _kindsRepository;
        private readonly DbRepository<Picture> _pictureRepository;
        private readonly DbRepository<Conference> _conferenceRepository;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService, IWebHostEnvironment appEnvironment, DbRepository<Picture> pictureRepository, DbRepository<Article> articleRepository, DbRepository<Team> teamRepository, DbRepository<KindOfSport> kindsRepository, DbRepository<Conference> conferenceRepository)
        {
            _pictureRepository = pictureRepository;
            _articleRepository = articleRepository;
            _teamRepository = teamRepository;
            _kindsRepository = kindsRepository;
            _appEnvironment = appEnvironment;
            _conferenceRepository = conferenceRepository;
            _adminService = adminService;
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
                Teams = _teamRepository.GetAllAsync().Result,
                Conferences = _conferenceRepository.GetAllAsync().Result,
                KindsOfSport = _kindsRepository.GetAllAsync().Result,
            };

            return View(model); 
        }


        [HttpPost]
        public IActionResult Article(string Conference, string Location, string Kindofsport, string Team, string Alt, string Headline, string Caption, string Content, string Filter, string Size, string Crop, IFormFile Logo)
        {
            KindOfSport kindOfSport = _kindsRepository.Find(x => x.Name == Kindofsport).FirstOrDefault();
            Team team = _teamRepository.Find(x => x.Name == Team).FirstOrDefault();
            Conference conference = _conferenceRepository.Find(x => x.Name == Conference).FirstOrDefault();

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
                Filter = "filter: " + Filter + ";",
                Size = "height: " + Size.Split(" ")[0] + "; width: " + Size.Split(" ")[1] + ";",
                Crop = Crop,
            }).Wait();

            Picture picture = _pictureRepository.Find(x => logoPath == x.LogoPath).FirstOrDefault();

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
                KindOfSport = kindOfSport ,
                Team = team,
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

        [HttpGet]
        [Route("articles-list")]
        public async Task<ActionResult<AdminArticleViewModel>> ArticlesListAsync(string filterConference, string filterTeam, string filterStatus, string searchString)
        {
            try
            {
                await _adminService.FilterInitializerAsync(HttpContext.Session, filterConference, filterTeam, filterStatus, searchString);
                AdminArticleViewModel articlesToViewModels = _adminService.ViewModelInitializerAsync(HttpContext.Session).Result;
                await _adminService.FilterArticlesAsync(articlesToViewModels, HttpContext.Session);
                return View("articles_list", articlesToViewModels);
            }
            catch (Exception e)
            {
                //TODO Logging
                throw;
            }
        }

        [Route("delete-article"), ActionName("DeleteArticle")]
        [HttpPost]
        public async Task<ActionResult> DeleteArticleAsync(int id)
        {
            try
            {
                await _adminService.DeleteArticleByIdAsync(id);
                return Ok();
            }
            catch (Exception e)
            {
                //TODO Logging
                return BadRequest("There are no article to delete for such id");
            }
        }

        [Route("change-article-status"), ActionName("ChangeArticleStatus")]
        [HttpPost]
        public async Task<ActionResult> ChangeArticleStatusAsync(int id)
        {
            try
            {
                await _adminService.ChangeArticleStatusByIdAsync(id);
                return RedirectToAction("ArticlesList", "Admin");
            }
            catch (Exception e)
            {
                //TODO Logging
                return BadRequest("Something went wrong");
            }
        }

        [Route("change-article-sport"), ActionName("ChangeArticleKindOfSport")]
        [HttpPost]
        public async Task<ActionResult> ChangeArticleKindOfSportByIdAsync(int articleId, int kindOfSportId)
        {
            try
            {
                await _adminService.ChangeArticleKindOfSportByIdAsync(articleId, kindOfSportId);
                return RedirectToAction("ArticlesList", "Admin");
            }
            catch (Exception e)
            {
                //TODO Logging
                return BadRequest("Something went wrong");
            }
        }
    }
}