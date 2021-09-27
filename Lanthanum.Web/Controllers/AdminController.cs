using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Lanthanum.Web.Models;
using Lanthanum.Web.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;

namespace Lanthanum.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("[controller]")]
    public class AdminController : Controller
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
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
