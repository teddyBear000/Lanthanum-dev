using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Lanthanum.Web.Controllers
{
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IMapper _mapper;

        public AdminController(IAdminService adminService, IMapper mapper)
        {
            _adminService = adminService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("articles-list")]
        public async Task<ActionResult<AdminArticleViewModel>> ArticlesList(string filterConference, string filterTeam, string filterStatus, string searchString)
        {
            var articles = await _adminService.GetAllArticlesAsync();

            if (articles != null)
            {
                if(!string.IsNullOrEmpty(filterConference)) { HttpContext.Session.SetString("FilterConference", filterConference); }
                if (!string.IsNullOrEmpty(filterTeam)) { HttpContext.Session.SetString("FilterTeam", filterTeam); }
                if (!string.IsNullOrEmpty(filterStatus)) { HttpContext.Session.SetString("FilterStatus", filterStatus); }
                HttpContext.Session.SetString("SearchString", !string.IsNullOrEmpty(searchString) ? searchString : "");

                var simpleModels = _mapper.Map<IEnumerable<HelperAdminArticleViewModel>>(articles);
                AdminArticleViewModel articlesToViewModels = new AdminArticleViewModel()
                {
                    SimpleModels = simpleModels,
                    FilterConference = HttpContext.Session.GetString("FilterConference"),
                    FilterStatus = HttpContext.Session.GetString("FilterStatus"),
                    FilterTeam = HttpContext.Session.GetString("FilterTeam"),
                    SearchString = HttpContext.Session.GetString("SearchString"),
                    Conferences = simpleModels.Select(a => a.TeamConference).Distinct(),
                    TeamNames = simpleModels.Select(a => a.TeamName).Distinct(),
                    KindsOfSport = _adminService.GetAllKindsOfSportNamesAsync().Result
                };

                _adminService.FilterArticles(
                    articlesToViewModels,
                    HttpContext.Session.GetString("FilterConference"),
                    HttpContext.Session.GetString("FilterTeam"),
                    HttpContext.Session.GetString("FilterStatus"),
                    HttpContext.Session.GetString("SearchString"));

                return View("articles_list", articlesToViewModels);
            }
            return BadRequest("There are no articles");
        }

        [Route("delete-article"), ActionName("DeleteArticle")]
        [HttpPost]
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

        [Route("change-article-sport"), ActionName("ChangeArticleKindOfSport")]
        [HttpPost]
        public async Task<ActionResult> ChangeArticleKindOfSportByIdAsync(int articleId, int kindOfSportId)
        {
            try
            {
                await _adminService.ChangeArticleKindOfSportByIdAsync(articleId, kindOfSportId);
                return RedirectToAction("ArticlesList", "Admin", new { searchString = TempData?.Peek("searchString") });
            }
            catch (Exception e)
            {
                //TODO Logging
                return BadRequest("Something went wrong");
            }
        }
    }
}
