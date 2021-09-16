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
        [Route("articles-list"),ActionName("ArticlesList")]
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
                }).ToList();

                ViewData["TeamNames"] = articlesToViewModels.Select(a => a.TeamName).Distinct();
                ViewData["Conferences"] = articlesToViewModels.Select(a => a.TeamConference).Distinct();
                _adminService.FilterArticles(ref articlesToViewModels, filterConference, filterTeam, filterStatus);

                return View("articles_list",articlesToViewModels);
            }
            return BadRequest("There are no articles");
        }

        [HttpPost]
        [Route("delete-article"), ActionName("DeleteArticle")]
        public async Task<ActionResult> DeleteArticleAsync(int id)
        {
            await _adminService.DeleteArticleByIdAsync(id);
            try
            {
                return Ok();
                //return RedirectToAction("ArticlesList","Admin");
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
                return NoContent();
                //return RedirectToAction("ArticlesList", "Admin");
            }
            catch (Exception e)
            {
                //TODO Logging
                return BadRequest("Something went wrong");
            }
        }
    }
}
