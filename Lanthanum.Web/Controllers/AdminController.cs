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
            if(filterConference!=null) { TempData["FilterConference"] = filterConference; }
            if (filterTeam!=null) { TempData["FilterTeam"] = filterTeam; }
            if (filterStatus!=null) { TempData["FilterStatus"] = filterStatus; }

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

                ViewData["TeamNames"] = articlesToViewModels.Select(a => a.TeamName).Distinct();
                ViewData["Conferences"] = articlesToViewModels.Select(a => a.TeamConference).Distinct();

                _adminService.FilterArticles(
                    ref articlesToViewModels, 
                    (string)TempData?.Peek("FilterConference"),
                    (string)TempData?.Peek("FilterTeam"),
                   (string) TempData?.Peek("FilterStatus"));
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
