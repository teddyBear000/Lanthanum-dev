using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using Lanthanum.Web.Models;
using System.Threading.Tasks;

namespace Lanthanum.Web.Controllers
{
    public class ArticlesController : Controller
    {

        private readonly ILogger<ArticlesController> _logger;
        private readonly DbRepository<Comment> _commentRepository;
        private readonly DbRepository<User> _userRepository;
        private readonly DbRepository<Article> _articleRepository;

        public ArticlesController(ILogger<ArticlesController> logger, DbRepository<Comment> commentRepository, DbRepository<User> userRepository, DbRepository<Article> articleRepository)
        {
            _logger = logger;
            _commentRepository = commentRepository;
            _userRepository = userRepository;
            _articleRepository = articleRepository;
        }

        public IActionResult Details(int id)
        {
            var article =  _articleRepository.GetByIdAsync(id).Result;
            var comments = _commentRepository
                .Find(x => x.Article == _articleRepository.GetByIdAsync(id).Result)
                .OrderByDescending(x => x.DateTimeOfCreation)
                .ToList();
            var users = _userRepository.GetAllAsync().Result.ToList();
            var currentUser = _userRepository.SingleOrDefaultAsync(x => x.Email == User.Identity.Name).Result;
            List<Article> articleList = _articleRepository.GetAllAsync().Result.ToList();

            ViewBag.Article = article;
            ViewBag.Comments = comments;
            ViewBag.Users = users;
            ViewBag.CurrentUser = currentUser;
            ViewBag.ModelArticle = article;
            ViewBag.ModelArticles = new List<Article>() { articleList[0], articleList[0], articleList[0], articleList[0], articleList[0], articleList[0] };
           
            return View();
        }


        [Authorize]
        public IActionResult AddComment(string commentContent, int articleId,int parentCommentId=-1)
        {
            var comment = new Comment
            {
                Content = commentContent,
                Author = _userRepository.SingleOrDefaultAsync(x => x.Email == User.Identity.Name).Result,
                Article = _articleRepository.GetByIdAsync(articleId).Result,
                ParentComment = _commentRepository.GetByIdAsync(parentCommentId).Result
            };
            _commentRepository.AddAsync(comment).Wait();
            return RedirectToAction("Details", new { id = articleId });
        }
    }
}