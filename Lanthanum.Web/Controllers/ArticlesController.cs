using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Lanthanum.Web.Data.Repositories;
using Lanthanum.Web.Domain;
using Lanthanum.Web.Models;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using System.Linq;
using Lanthanum.Web.Services.Interfaces;

namespace Lanthanum.Web.Controllers
{
    public class ArticlesController : Controller
    {

        private readonly ILogger<ArticlesController> _logger;
        private readonly DbRepository<Article> _articleRepository;
        private readonly ICommentService _commentService;
        public ArticlesController(ILogger<ArticlesController> logger,DbRepository<Article> articleRepository, ICommentService commentService)
        {
            _logger = logger;
            _articleRepository = articleRepository;
            _commentService = commentService;
        }
        
        public IActionResult Details(int id, string commentSortingMethod = " ")
        {
            Article article = _articleRepository.GetByIdAsync(id).Result;
            List<Comment> comments = _commentService.GetCurrentArticleComments(article);
            List<Reaction> reactions = _commentService.GetCommentReactions(comments);
            List<User> users = _commentService.GetCommentAuthors(comments);
            List<Comment> sortedComments = _commentService.SortComments(comments, commentSortingMethod);
            List<Article> articleList = _articleRepository.GetAllAsync().Result.ToList();
            string currentUserImage = _commentService.GetCurrentUserImage(User.Identity.Name);
            var model = new ArticleViewModel
            {
                ReactionToComments = reactions,
                MainArticle = article,
                Comments = sortedComments,
                Users = users,
                CurrentUserImage = currentUserImage,
                MoreArticlesSection = new List<Article>() { articleList[0], articleList[0], articleList[0], articleList[0], articleList[0], articleList[0] }
            };  
            return View(model);
        }

        [Authorize]
        public IActionResult AddComment(string commentContent, int articleId, int parentCommentId = -1)
        {
            _commentService.AddComment(commentContent, articleId, User.Identity.Name, parentCommentId);
            return RedirectToAction("Details", new { id = articleId });
        }

        [Authorize]
        public IActionResult DeleteComment(int articleId, int commentId) 
        {
            _commentService.DeleteComment(commentId);
            return RedirectToAction("Details", new { id = articleId });
        }

        [Authorize]
        public IActionResult ManageReaction(int articleId, int commentId, int reactionPoint)
        {
            _commentService.ReactionManager(User.Identity.Name, commentId, reactionPoint);
            return RedirectToAction("Details", new { id = articleId });
        }

        [Authorize]
        public IActionResult EditComment(int articleId, int commentId, string commentNewContent) 
        {
            _commentService.EditComment(commentId, commentNewContent);
            return RedirectToAction("Details", new { id = articleId });
        }
    }
}