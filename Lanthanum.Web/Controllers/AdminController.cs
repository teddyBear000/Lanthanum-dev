using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services.Interfaces;
using Microsoft.AspNetCore.Routing;

namespace Lanthanum.Web.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        //[HttpGet]
        ////public IActionResult Article() => View();
        //[HttpPost]
        //public IActionResult Article(string name) => Content($"{name}");


        [HttpGet]
        [Route("articles-list")]
        public async Task<ActionResult<IEnumerable<ArticleViewModel>>> ArticlesList(string filterConference,string filterTeam,string filterStatus)
        {
            ViewData["FilterConference"] = filterConference;
            ViewData["FilterTeam"] = filterTeam;
            ViewData["FilterStatus"] = filterStatus;
            var articles = await _adminService.GetAllArticlesAsync();

            if (articles != null)
            {
                IEnumerable<ArticleViewModel> articlesToViewModels = articles.Select(article => new ArticleViewModel
                {
                    Id = article.Id,
                    Alt = article.Alt,
                    Headline = article.Headline,
                    LogoPath = article.LogoPath,
                    Caption = article.Caption,
                    MainText = article.MainText,
                    TeamConference = article.Team.Conference,
                    TeamLocation = article.Team.Location,
                    TeamName = article.Team.Name,
                    ArticleStatus = article.ArticleStatus
                });

                ViewData["TeamNames"] = articles.Select(a => a.Team.Name).Distinct();
                ViewData["Conferences"] = articles.Select(a => a.Team.Conference).Distinct();

                _adminService.FilterArticles(ref articlesToViewModels, filterConference, filterTeam, filterStatus);
                return View("articles_list",articlesToViewModels);
            }
            return BadRequest("There are no articles");
        }

        [HttpPost]
        [Route("delete-article"), ActionName("DeleteArticle")]
        public async Task<ActionResult> DeleteArticleAsync(int id)
        {
            try
            {
                await _adminService.DeleteArticleByIdAsync(id);
                return RedirectToAction("ArticlesList","Admin");
            }
            catch (Exception e)
            {
                //TODO Logging
                return BadRequest("There are no article to delete for such id");
            }
        }

        [Route("change-article-status"),ActionName("ChangeArticleStatus")]
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
    }
}
